using System.Net.Http;
using System.Threading.Tasks;

namespace Turkcell.Updater
{
    internal class TurkcellUpdaterClient : HttpClient
    {
        public static readonly TurkcellUpdaterClient Instance = new TurkcellUpdaterClient();

        private TurkcellUpdaterClient()
        {
            DefaultRequestHeaders.Add("User-Agent", "TurkcellUpdater/" + Configuration.ProductVersion.Major + "." + Configuration.ProductVersion.Minor);
        }

        internal async Task<TurkcellUpdaterResponse> RequestAsync(VersionMapRequest request)
        {
            var httpResponseMessage = await SendAsync(request);
            request.Response = httpResponseMessage;
            return await request.ToResponseAsync();
        }
    }
}