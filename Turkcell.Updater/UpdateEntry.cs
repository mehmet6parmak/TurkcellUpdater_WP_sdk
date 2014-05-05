using System;
using System.Collections.Generic;
using LitJson;
using Turkcell.Updater.LitJson;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater
{
    internal class UpdateEntry : FilteredEntry
    {
        internal readonly List<UpdateDescription> UpdateDescriptions;
        private readonly bool _forceExit;
        private readonly bool _forceUpdate;
        private readonly Uri _targetAppUriSchema;
        private readonly bool _targetMarketplace;
        private readonly String _targetPackageId;
        private readonly Version _targetVersionCode;
        private readonly Uri _targetWebsiteUri;

        internal UpdateEntry(List<Filter> filters,
                             List<UpdateDescription> updateDescriptions, Version targetVersionCode,
                             String targetPackageName, Uri targetAppUriSchema, bool targetMarketplace, bool forceUpdate,
                             bool forceExit)
            : base(filters)
        {
            UpdateDescriptions = updateDescriptions;
            _targetVersionCode = targetVersionCode;
            _targetPackageId = targetPackageName;
            _targetAppUriSchema = targetAppUriSchema;
            _targetMarketplace = targetMarketplace;
            _forceUpdate = forceUpdate;
            _forceExit = forceExit;
        }

        internal UpdateEntry(JsonData jsonObject)
            : base(jsonObject)
        {
            _targetVersionCode = jsonObject.OptVersion("targetVersionCode");
            _forceUpdate = jsonObject.OptBoolean("forceUpdate");
            _forceExit = jsonObject.OptBoolean("forceExit");
            UpdateDescriptions = CreateUpdateDescritions(jsonObject);
            _targetPackageId = StringUtils.RemoveWhiteSpaces(jsonObject.OptString("targetPackageName"));
            _targetMarketplace = jsonObject.OptBoolean("targetGooglePlay");


            string targetSchema = jsonObject.OptString("targetUriSchema");
            if (!String.IsNullOrEmpty(targetSchema) && Uri.IsWellFormedUriString(targetSchema, UriKind.Absolute))
            {
                _targetAppUriSchema = new Uri(targetSchema);
            }

            string targetWebSite = jsonObject.OptString("targetWebsiteUrl");
            if (!String.IsNullOrEmpty(targetWebSite) && Uri.IsWellFormedUriString(targetWebSite, UriKind.Absolute))
            {
                _targetWebsiteUri = new Uri(targetWebSite);
            }
        }

        private static List<UpdateDescription> CreateUpdateDescritions(
            JsonData jsonObject)
        {
            var result = new List<UpdateDescription>();
            JsonData udsObject = jsonObject.OptJsonData("descriptions");

            if (udsObject != null)
            {
                ICollection<string> languages = udsObject.Keys;
                foreach (string language in languages)
                {
                    JsonData o = udsObject.OptJsonData(language);
                    if (o != null)
                    {
                        var ud = new UpdateDescription(language, o);
                        result.Add(ud);
                    }
                    else
                    {
                        String s = udsObject.OptString(language, null);
                        if (s != null)
                        {
                            var ud = new UpdateDescription(language, s);
                            result.Add(ud);
                        }
                    }
                }
            }
            return result;
        }

        internal Update GetUpdate(Properties properties)
        {
            String languageCode = null;
            if (properties != null)
            {
                String s = properties[Properties.KeyDeviceLanguage];
                if (!String.IsNullOrEmpty(s))
                {
                    languageCode = s;
                }
            }

            String packageName = _targetPackageId;
            if (String.IsNullOrEmpty(packageName) && properties != null
                && _targetAppUriSchema == null
                && _targetWebsiteUri == null)
            {
                packageName = properties[Properties.KeyAppPackageId];
            }
            //TODO ask what we should do here? If no appId, notargetschema or no targetwebsite provided 
            //do i need to open marketplace details for the current app?
            //if (String.IsNullOrEmpty(packageName))
            //{
            //    throw new UpdaterException("'packageName' property should not be null or empty.");
            //}

            UpdateDescription updateDescription = LocalizedStringMap.Select(UpdateDescriptions, languageCode);
            return new Update(updateDescription, _targetMarketplace, _targetVersionCode, _targetAppUriSchema,
                              _targetWebsiteUri,
                              packageName, _forceUpdate, _forceExit);
        }

        internal bool ShouldDisplay(Properties properties)
        {
            if (IsMatches(properties))
            {
                Version currentVersion = Version.Parse(properties[Properties.KeyAppVersion]);
                if (currentVersion != null && _targetVersionCode != currentVersion)
                {
                    return true;
                }
                String currentPackageName = properties[Properties.KeyAppPackageId];
                if (!String.IsNullOrEmpty(currentPackageName) && !String.IsNullOrEmpty(_targetPackageId))
                {
                    return !currentPackageName.Equals(_targetPackageId);
                }
            }

            return false;
        }
    }
}