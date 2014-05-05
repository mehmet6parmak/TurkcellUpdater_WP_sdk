using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Turkcell.Updater.Utility
{
    internal static class CapabilityHelper
    {
        private const string WmAppManifest = "WMAppManifest.xml";
        private const string IdCapNetworking = "ID_CAP_NETWORKING";
        private const string IdCapIdentityDevice = "ID_CAP_IDENTITY_DEVICE";
        private const string IdCapIdentityUser = "ID_CAP_IDENTITY_USER";
        private const string IdCapLocation = "ID_CAP_LOCATION";
        private const string IdCapSensors = "ID_CAP_SENSORS";
        private const string IdCapMicrophone = "ID_CAP_MICROPHONE";
        private const string IdCapMedialib = "ID_CAP_MEDIALIB";
        private const string IdCapGamerservices = "ID_CAP_GAMERSERVICES";
        private const string IdCapPhonedialer = "ID_CAP_PHONEDIALER";
        private const string IdCapPushNotification = "ID_CAP_PUSH_NOTIFICATION";
        private const string IdCapWebbrowsercomponent = "ID_CAP_WEBBROWSERCOMPONENT";
        private const string Capabilities = "Capabilities";
        private const string Name = "Name";

        static CapabilityHelper()
        {
            using (Stream strm = TitleContainer.OpenStream(WmAppManifest))
            {
                XElement xml = XElement.Load(strm);
                IEnumerable<XElement> capabilities = xml.Descendants(Capabilities).Elements();

                XElement[] xElements = capabilities as XElement[] ?? capabilities.ToArray();
                IsNetworkingCapability = CheckCapability(xElements, IdCapNetworking);
                IsDeviceIdentityCapability = CheckCapability(xElements, IdCapIdentityDevice);
                IsUserIdentityCapability = CheckCapability(xElements, IdCapIdentityUser);
                IsLocationCapability = CheckCapability(xElements, IdCapLocation);
                IsSensorsCapability = CheckCapability(xElements, IdCapSensors);
                IsMicrophoneCapability = CheckCapability(xElements, IdCapMicrophone);
                IsMediaLibCapability = CheckCapability(xElements, IdCapMedialib);
                IsGamerServicesCapability = CheckCapability(xElements, IdCapGamerservices);
                IsPhoneDialerCapability = CheckCapability(xElements, IdCapPhonedialer);
                IsPushNotificationCapability = CheckCapability(xElements, IdCapPushNotification);
                IsWebBrowserComponentCapability = CheckCapability(xElements, IdCapWebbrowsercomponent);
            }
        }

        public static bool IsNetworkingCapability { get; set; }
        public static bool IsDeviceIdentityCapability { get; set; }
        public static bool IsUserIdentityCapability { get; set; }
        public static bool IsLocationCapability { get; set; }
        public static bool IsSensorsCapability { get; set; }
        public static bool IsMicrophoneCapability { get; set; }
        public static bool IsMediaLibCapability { get; set; }
        public static bool IsGamerServicesCapability { get; set; }
        public static bool IsPhoneDialerCapability { get; set; }
        public static bool IsPushNotificationCapability { get; set; }
        public static bool IsWebBrowserComponentCapability { get; set; }

        private static bool CheckCapability(IEnumerable<XElement> capabilities, string capabilityName)
        {
            XElement capability = capabilities.FirstOrDefault(n =>
                {
                    XAttribute xAttribute = n.Attribute(Name);
                    return xAttribute != null && xAttribute.Value.Equals(capabilityName);
                });
            return capability != null;
        }
    }
}