using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using LitJson;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater
{
    /// <summary>
    ///     Represents Device and application properties.
    ///     Following property values are automatically retrieved:
    ///     <ul>
    ///         <li>
    ///             <see cref="KeyAppPackageId" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyAppVersion" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceOsName" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceOsVersion" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceBrand" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceId" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceIsTablet" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceLanguage" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceMcc" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceMnc" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceModel" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyDeviceScreenResolution" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyUpdaterLevel" />
    ///         </li>
    ///     </ul>
    ///     <br />
    ///     These values can be overriden by indexer using the corresponding key for each property.
    ///     Also it is possible to introduce new key-value pairs using <see cref="KeyCustomPrefix" />.
    /// </summary>
    public class Properties
    {
        /// <summary>
        ///     Id of the application.<br />
        ///     Example value: "{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}"
        /// </summary>
        public const String KeyAppPackageId = "appPackageName";

        /// <summary>
        ///     Version of the application defined in WMAppManifest.xml file.<br />
        ///     Example value: "1.0.0".
        /// </summary>
        public const String KeyAppVersion = "appVersionCode";


        /// <summary>
        ///     Name of the operating system of device.<br />
        ///     Value: "wp".
        ///     <br />
        ///     <strong>Overriding value of this key is not recommended.</strong>
        /// </summary>
        public const String KeyDeviceOsName = "deviceOsName";

        /// <summary>
        ///     Version of the operating system of device.<br />
        ///     Example value: "8.0.9903.0".
        /// </summary>
        public const String KeyDeviceOsVersion = "deviceOsVersion";

        /// <summary>
        ///     Brand name of device.<br />
        ///     Example value: "NOKIA".
        /// </summary>
        public const String KeyDeviceBrand = "deviceBrand";

        /// <summary>
        ///     Model name of device.<br />
        ///     Example value: "RM-821_eu_turkey_244" for Nokia Lumia 920
        /// </summary>
        public const String KeyDeviceModel = "deviceModel";


        /// <summary>
        ///     "true" if devices is a tablet, otherwise "false". For now
        ///     <value>False</value>
        ///     is returned because no tablets available with WP OS.
        ///     Example values: "true", "false".
        /// </summary>
        public const String KeyDeviceIsTablet = "deviceIsTablet";

        /// <summary>
        ///     Two letter language code of device
        ///     (see: <a href="http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes">ISO 639-1</a>).<br />
        ///     Example values: "en", "tr", "fr".
        /// </summary>
        public const String KeyDeviceLanguage = "deviceLanguage";

        /// <summary>
        ///     "x-" is prefix for application defined keys of arbitrary properties.<br />
        ///     Applications may define and add own custom property key-value pairs for application specific filters.<br />
        ///     Example values: "x-foo", "x-bar".
        /// </summary>
        public const String KeyCustomPrefix = "x-";


        /// <summary>
        ///     Mobile country code of device. See <a href="http://en.wikipedia.org/wiki/Mobile_country_code">Mobile country code</a><br />
        ///     Only available value is "286" which corresponds to Turkey since WP OS does allow developer to get real value from SIM card.
        /// </summary>
        public const String KeyDeviceMcc = "deviceMcc";

        /// <summary>
        ///     Mobile network code of device. See <a href="http://en.wikipedia.org/wiki/Mobile_country_code">Mobile country code</a><br />
        ///     Example value: "01" for Turkcell.
        ///     <br />
        ///     <strong>Note: </strong> Only operators operating in Turkey in the development time of this SDK are available. These are Turkcell, Vodafone and Avea.
        /// </summary>
        public const String KeyDeviceMnc = "deviceMnc";

        /// <summary>
        ///     A unique  alphanumeric identifier for device.<br />
        ///     Example value: "5e5aC2coeO0UuPY/nH/C3DdelqE4MuTkywh2aB9PT84"
        /// </summary>
        public const String KeyDeviceId = "deviceId";

        /// <summary>
        ///     An integer number that is used to define updater version used by application.<br />
        ///     Example value: "1" for initial version of updater sdk.
        ///     <br />
        ///     <strong>Overriding value of this key is not recommended.</strong>
        /// </summary>
        public const String KeyUpdaterLevel = "updaterLevel";

        /// <summary>
        ///     Resolution of the screen in pixels.
        /// </summary>
        public const string KeyDeviceScreenResolution = "deviceScreenResolution";

        private readonly Dictionary<String, String> _map;

        /// <summary>
        ///     Creates an instance and to retrive current device and application properties please call async
        ///     <see
        ///         cref="InitAsync()" />
        ///     method.
        /// </summary>
        private Properties()
            : this(new Dictionary<string, string>())
        {
        }

        private Properties(Dictionary<String, String> map)
        {
            _map = map;
        }

        /// <summary>
        ///     Sets or Returns value of given <strong>key</strong>
        /// </summary>
        /// <param name="key">Key for the value.</param>
        /// <returns>Value for the given Key or String.Empty if does not exist.</returns>
        public string this[string key]
        {
            get { return _map.ContainsKey(key) ? _map[key] : String.Empty; }
            set { _map[key] = value; }
        }

        /// <summary>
        ///     Creates an instance of <see cref="Properties" /> class and fetches some device specific information automatically.
        /// </summary>
        /// <param name="map">Additional key-value pairs to be sent to server.</param>
        /// <returns></returns>
        public static async Task<Properties> CreateInstance(Dictionary<string, string> map = null)
        {
            Properties properties = map == null ? new Properties() : new Properties(map);
            await properties.InitAsync();
            return properties;
        }

        /// <summary>
        ///     Fetches Application and Platform information asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="Task" /> instance for the operation.
        /// </returns>
        public async Task<Properties> InitAsync()
        {
            await AutoFetch();
            return this;
        }

        private async Task AutoFetch()
        {
            var parameters = new PlatformParameters();
            await parameters.Retrieve();

            this[KeyAppPackageId] = parameters.AppId;
            this[KeyAppVersion] = parameters.AppVersion;

            this[KeyDeviceBrand] = parameters.DeviceBrand;
            this[KeyDeviceModel] = parameters.DeviceModel;
            this[KeyDeviceIsTablet] = Boolean.FalseString;
            this[KeyDeviceLanguage] = parameters.DeviceLanguage;
            this[KeyDeviceMcc] = parameters.DeviceMcc;
            this[KeyDeviceMnc] = parameters.DeviceMnc;
            this[KeyUpdaterLevel] = Configuration.Level.ToString(CultureInfo.InvariantCulture);
            this[KeyDeviceOsName] = "wp";
            this[KeyDeviceOsVersion] = parameters.DeviceOsVersion;
            this[KeyDeviceId] = parameters.DeviceId;
            this[KeyDeviceScreenResolution] = String.Format("{0}x{1}", parameters.DeviceScreenHeight,
                                                            parameters.DeviceScreenWidth);
        }

        /// <summary>
        ///     Returns a new <see cref="Dictionary{String,String}" /> instance containing all the Key-Value pairs.
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> ToDictionary()
        {
            return new Dictionary<string, string>(_map);
        }

        internal string ToJson()
        {
            var data = new JsonData();
            foreach (var value in _map)
            {
                data[value.Key] = value.Value;
            }
            return data.ToJson();
        }
    }
}