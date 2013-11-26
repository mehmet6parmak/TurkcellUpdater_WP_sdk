using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Info;
using System.Globalization;
using System.Diagnostics;

namespace Turkcell.Updater.Utility
{
    internal class PlatformParameters
    {
        public bool IsTestModeEnabled { get; protected set; }
        public string DeviceBrand { get; protected set; }
        public string DeviceModel { get; protected set; }
        public string DeviceId { get; protected set; }

        private static string GetDeviceId()
        {
            String deviceId = null;
            try
            {
                if (CapabilityHelper.IsDeviceIdentityCapability)
                {
                    var deviceIdBytes = DeviceExtendedProperties.GetValue("DeviceUniqueId") as byte[];
                    if (deviceIdBytes != null)
                        deviceId = Convert.ToBase64String(deviceIdBytes);
                    if (!String.IsNullOrEmpty(deviceId))
                        deviceId = deviceId.Trim('{', '}', ' ');
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Error getting device ID: " + err.Message);
            }
            return deviceId;
        }

        public string DeviceMcc { get; protected set; }
        public string DeviceMnc { get; protected set; }
        public string AppId { get; protected set; }
        public string PublisherId { get; protected set; }
        public string DeviceOsName { get; protected set; }
        public string DeviceOsVersion { get; protected set; }
        public int? DeviceScreenWidth { get; protected set; }
        public int? DeviceScreenHeight { get; protected set; }
        public string AppVersion { get; protected set; }
        public string AppInstallationId { get; protected set; }
        public string DeviceLanguage { get; protected set; }

        public async Task Retrieve()
        {
            await Task.Run(async () =>
                {
                    try
                    {

#if DEBUG
                        IsTestModeEnabled = true;
#endif
                        if (Microsoft.Devices.Environment.DeviceType == DeviceType.Emulator)
                        {
                            IsTestModeEnabled = true;
                        }

                        DeviceBrand = DeviceStatus.DeviceManufacturer;
                        DeviceModel = DeviceStatus.DeviceName;

                        var operatorInfo = OperatorInfoHelper.GetOperatorInfo();
                        if (operatorInfo != null)
                        {
                            DeviceMcc = operatorInfo.MobileCountryCode;
                            DeviceMnc = operatorInfo.MobileNetworkCode;
                        }
                        AppId = WmAppManifestHelper.GetAppId();
                        PublisherId = WmAppManifestHelper.GetPublisherId();

                        DeviceOsName = "WP";
                        DeviceOsVersion = System.Environment.OSVersion.Version.ToString();

                        DeviceLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                        AppVersion = WmAppManifestHelper.GetAppVersion();
                        DeviceId = GetDeviceId();

                        AppInstallationId = await ReadAppInstallationId();


                        //throws invalid cross thread access exception in unit tests.
                        DeviceScreenWidth =
                            (int)
                            (Math.Ceiling(Application.Current.Host.Content.ScaleFactor / 100.0 *
                                          Application.Current.Host.Content.ActualWidth));
                        DeviceScreenHeight =
                            (int)
                            (Math.Ceiling(Application.Current.Host.Content.ScaleFactor / 100.0 *
                                          Application.Current.Host.Content.ActualHeight));

                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine(err.Message + System.Environment.NewLine + err.StackTrace);
                    }
                });
        }

        private const string AppInstallationIdFilename = ".updaterSdkInstallationId";
        private async Task<string> ReadAppInstallationId()
        {
            string id = String.Empty;
            if (await IsolatedStorageHelper.FileExistsUnderLocalFolderAsync(AppInstallationIdFilename))
                id = await IsolatedStorageHelper.ReadFileFromLocalFolder(AppInstallationIdFilename);
            if (String.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
                await IsolatedStorageHelper.WriteToLocalFileAsync(AppInstallationIdFilename, id);
            }
            return id;
        }
    }
}
