using System;
using System.Xml;

namespace Turkcell.Updater.Utility
{
    internal class WmAppManifestHelper
    {
        private const string PublisherIdAttributeName = "PublisherID";
        private const string AppManifestName = "WMAppManifest.xml";
        private const string AppNodeName = "App";
        private const string AppProductIdAttributeName = "ProductID";
        private const string AppVersionAttributeName = "Version";

        public static string BuildApplicationDeepLink()
        {
            Guid applicationId = Guid.Parse(GetManifestAttributeValue(AppProductIdAttributeName));

            return BuildApplicationDeepLink(applicationId.ToString());
        }

        public static string GetAppVersion()
        {
            return GetManifestAttributeValue(AppVersionAttributeName);
        }

        public static string GetAppId()
        {
            return GetManifestAttributeValue(AppProductIdAttributeName);
        }

        public static string GetPublisherId()
        {
            return GetManifestAttributeValue(PublisherIdAttributeName);
        }

        public static string BuildApplicationDeepLink(string applicationId)
        {
            return @"http://windowsphone.com/s?appid=" + applicationId;
        }

        public static string GetManifestAttributeValue(string attributeName)
        {
            var xmlReaderSettings = new XmlReaderSettings
                {
                    XmlResolver = new XmlXapResolver()
                };

            using (XmlReader xmlReader = XmlReader.Create(AppManifestName, xmlReaderSettings))
            {
                xmlReader.ReadToDescendant(AppNodeName);

                if (!xmlReader.IsStartElement())
                {
                    throw new FormatException(AppManifestName + " is missing " + AppNodeName);
                }

                return xmlReader.GetAttribute(attributeName);
            }
        }
    }
}