using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LitJson;
using Turkcell.Updater.LitJson;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater
{
    internal class VersionMapRequest : HttpRequestMessage
    {
        public VersionMapRequest(Uri versionServerUri, Properties currentProperties, bool postProperties)
        {
            RequestUri = versionServerUri;
            CurrentProperties = currentProperties;
            PostProperties = postProperties;

            if (postProperties && currentProperties != null)
            {
                throw new NotImplementedException();
                Method = HttpMethod.Post;
                string json = currentProperties.ToJson();
                Content = new StringContent(json, Encoding.UTF8);
            }
            else
            {
                Method = HttpMethod.Get;
            }
        }

        public Properties CurrentProperties { get; private set; }
        public bool PostProperties { get; private set; }
        public HttpResponseMessage Response { get; set; }

        public async Task<TurkcellUpdaterResponse> ToResponseAsync()
        {
            HttpResponseMessage response = Response;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = string.Empty;
                if (response.Content != null)
                {
                    var streamContent = response.Content as StreamContent;
                    //Note: Configurations files has to be UTF-8 encoded.
                    content = await streamContent.ReadTextAsync();
                }
                JsonData jsonData = JsonMapper.ToObject(content);
                var result = new TurkcellUpdaterResponse(response.StatusCode);
                return FillResponseWithData(result, jsonData);
            }
            return new TurkcellUpdaterResponse(response.StatusCode);
        }

        public TurkcellUpdaterResponse FillResponseWithData(TurkcellUpdaterResponse response, JsonData jsonData)
        {
            if (jsonData != null)
            {
                try
                {
                    String packageName = CurrentProperties[Updater.Properties.KeyAppPackageId];
                    if (jsonData.ContainsKey("errorMessage"))
                    {
                        Log.E("Remote error: " + jsonData.OptString("errorMessage"));
                        response.Error = new UpdaterException("Remote error: " + jsonData.OptString("errorMessage"));
                    }

                    if (VersionsMap.IsVersionMapOfPackageId(packageName, jsonData))
                    {
                        var map = new VersionsMap(jsonData);
                        Update update = map.GetUpdate(CurrentProperties);
                        if (update != null)
                        {
                            Log.I("Update found: " + update);
                            response.Update = update;
                        }
                        else
                        {
                            var records = new MessageDisplayRecords();
                            Message message = map.GetMessage(CurrentProperties, records);
                            if (message == null)
                            {
                                Log.I("No update or message found.");
                            }
                            else
                            {
                                Log.I("Message found: " + message);
                                response.Message = message;
                            }
                        }
                    }
                    else
                    {
                        response.Error = new UpdaterException("Configuration file PackageId should be: " + packageName);
                    }
                }
                catch (Exception e)
                {
                    Log.D("Couldn't process update configuration file", e);
                    response.Error = new UpdaterException("Couldn't process update configuration file", e);
                }
            }
            return response;
        }
    }
}