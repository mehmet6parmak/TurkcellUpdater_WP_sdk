using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Info;
using Environment = Microsoft.Devices.Environment;

namespace Turkcell.Updater.Utility
{
    internal class PlatformParameters
    {
        private const string AppInstallationIdFilename = ".updaterSdkInstallationId";
        public bool IsTestModeEnabled { get; protected set; }
        public string DeviceBrand { get; protected set; }
        public string DeviceModel { get; protected set; }
        public string DeviceId { get; protected set; }

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

        internal async Task Retrieve()
        {
            await Task.Run(() =>
                {
                    try
                    {
#if DEBUG
                    IsTestModeEnabled = true;
#endif
                        if (Environment.DeviceType == DeviceType.Emulator)
                        {
                            IsTestModeEnabled = true;
                        }

                        DeviceBrand = DeviceStatus.DeviceManufacturer;
                        DeviceModel = DeviceStatus.DeviceName;

                        OperatorInfo operatorInfo = OperatorInfoHelper.GetOperatorInfo();
                        if (operatorInfo != null)
                        {
                            DeviceMcc = operatorInfo.MobileCountryCode;
                            DeviceMnc = operatorInfo.MobileNetworkCode;
                        }
                        AppId = WmAppManifestHelper.GetAppId();
                        PublisherId = WmAppManifestHelper.GetPublisherId();

                        DeviceOsName = "WP";
                        DeviceOsVersion = System.Environment.OSVersion.Version.ToString();


                        DeviceId = GetDeviceId();

                        Task<string> installationIdTask = ReadAppInstallationId();
                        installationIdTask.Wait();
                        AppInstallationId = installationIdTask.Result;
                        DeviceLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                        AppVersion = WmAppManifestHelper.GetAppVersion();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine(err.Message + System.Environment.NewLine + err.StackTrace);
                    }
                });
            DeviceScreenWidth =
                (int)
                (Math.Ceiling(Application.Current.Host.Content.ScaleFactor/100.0*
                              Application.Current.Host.Content.ActualWidth));
            DeviceScreenHeight =
                (int)
                (Math.Ceiling(Application.Current.Host.Content.ScaleFactor/100.0*
                              Application.Current.Host.Content.ActualHeight));
        }

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