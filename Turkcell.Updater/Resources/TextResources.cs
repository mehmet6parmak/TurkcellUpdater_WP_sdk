using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Windows.System;

namespace Turkcell.Updater.Resources
{
    /// <summary>
    /// Class holding text resources. You can override these texts simply providing new values through index providing the defined Key value. 
    /// 
    /// <pre>
    /// <code>
    /// 
    /// TextResources.Instance[TextResources.KeyInstall] = "Yükle";
    /// 
    /// </code>
    /// </pre>
    /// 
    /// 
    /// </summary>
    public class TextResources
    {
        private static readonly Dictionary<string, string> TurkishResources = new Dictionary<string, string>
            {
                {KeyClose, "Kapat"},
                {KeyErrorOccured, "Hata oluştu"},
                {KeyExitApplication, "Uygulamadan çık"},
                {KeyInstall, "Kur"},
                {KeyLaunch, "Çalıştır"},
                {KeyRemindMeLater, "Sonra hatırlat"},
                {KeyServiceIsNotAvailable, "Hizmet kullanılamıyor"},
                {KeyUpdateFound, "Güncelleme bulundu"},
                {KeyUpdateRequired, "Güncelleme gerekli"},
                {KeyView, "Görüntüle"}
            };

        private static readonly Dictionary<string, string> EnglishResources = new Dictionary<string, string>
            {
                 {KeyClose, "Close"},
                {KeyErrorOccured, "Error occurred"},
                {KeyExitApplication, "Exit application"},
                {KeyInstall, "Install"},
                {KeyLaunch, "Launch"},
                {KeyRemindMeLater, "Remind me later"},
                {KeyServiceIsNotAvailable, "Service is not available"},
                {KeyUpdateFound, "Update found"},
                {KeyUpdateRequired, "Update required"},
                {KeyView, "View"}
            };

        /// <summary>
        /// Corresponding value is used in Dialog buttons which generally used to close the dialog.
        /// </summary>
        public const string KeyClose = "close";
        /// <summary>
        /// Corresponding value is showed to the user or logged when an unexpected error occured.
        /// </summary>
        public const string KeyErrorOccured = "error_occured";
        /// <summary>
        /// Corresponding value is used in Dialog buttons which are expected to call <see cref="Application.Terminate"/>. This buttons appear in ForceExit and ForceUpdate Update types.
        /// </summary>
        public const string KeyExitApplication = "exit_application";
        /// <summary>
        /// Corresponding value is used in Dialog buttons which generally launches a target app detail in Store application.
        /// </summary>
        public const string KeyInstall = "install";
        /// <summary>
        /// Corresponding value is used in Dialog buttons if a <see cref="Update.TargetAppUriSchema"/> is provided. Provided value will be passed to the <see cref="Launcher.LaunchUriAsync(System.Uri)"/>
        /// </summary>
        public const string KeyLaunch = "launch";
        /// <summary>
        /// Corresponding value is used in Dialog buttons in an optional Update.
        /// </summary>
        public const string KeyRemindMeLater = "remind_me_later";
        /// <summary>
        /// Corresponding value is used as a Dialog title when an applications lifetime is ended and does not serve anymore.
        /// </summary>
        public const string KeyServiceIsNotAvailable = "service_is_not_available";
        /// <summary>
        /// Corresponding value is used as a Dialog title when an Update found.
        /// </summary>
        public const string KeyUpdateFound = "update_found";
        /// <summary>
        ///  Corresponding value is used as a Dialog title when a ForceUpdate found.
        /// </summary>
        public const string KeyUpdateRequired = "update_required";
        /// <summary>
        ///  Corresponding value is used in Dialog button when a <see cref="Message.TargetWebsiteUrl"/>
        /// </summary>
        public const string KeyView = "view";

        /// <summary>
        /// Contains All Resources
        /// </summary>
        internal static readonly Dictionary<string, Dictionary<string, string>> ResourceMap = new Dictionary<string, Dictionary<string, string>>
            {                
                {"tr",TurkishResources},
                {"en", EnglishResources}
            };

        internal static string CurrentCulture;
        internal const string DefaultResourceCulture = "en";

        static TextResources()
        {
            CurrentCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        }

        /// <summary>
        /// The only Instance of <see cref="TextResources"/> class. 
        /// </summary>
        public static readonly TextResources Instance = new TextResources();

        private TextResources(){}

        /// <summary>
        /// Indexer for the CurrentCulture ResourceDictionary
        /// </summary>
        /// <param name="key">Key for the value.</param>
        public string this[string key]
        {
            get
            {
                if (ResourceMap.ContainsKey(CurrentCulture) && ResourceMap[CurrentCulture].ContainsKey(key))
                {                    
                    return ResourceMap[CurrentCulture][key];
                }
                if (ResourceMap[DefaultResourceCulture].ContainsKey(key))
                {
                    return ResourceMap[DefaultResourceCulture][key];
                }
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(key))
                {
                    if (ResourceMap.ContainsKey(CurrentCulture))
                    {
                        ResourceMap[CurrentCulture][key] = value;
                    }
                    else
                    {
                        ResourceMap[DefaultResourceCulture][key] = value;
                    }
                }
            }
        }
    }
}