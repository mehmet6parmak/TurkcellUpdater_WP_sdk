using System;
using System.Collections.Generic;
using LitJson;
using Turkcell.Updater.Controls;

namespace Turkcell.Updater
{
    /// <summary>
    ///     Container for texts that are displayed on message dialog.<br />
    ///     <ul>
    ///         <li>
    ///             <see cref="P:Turkcell.Updater.LocalizedStringMap.Item(System.String)" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyTitle" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyMessage" />
    ///         </li>
    ///         <li>
    ///             <see cref="KeyImageUrl" />
    ///         </li>
    ///     </ul>
    /// </summary>
    public class MessageDescription : LocalizedStringMap
    {
        /// <summary>
        ///     Key for dialog title
        ///     <see cref="P:Turkcell.Updater.LocalizedStringMap.Item(System.String)" />
        /// </summary>
        public const String KeyTitle = "title";

        /// <summary>
        ///     Key for text displayed inside dialog
        ///     <see cref="P:Turkcell.Updater.LocalizedStringMap.Item(System.String)" />
        /// </summary>
        public const String KeyMessage = "message";

        /// <summary>
        ///     Key for url of image that displayed in dialog
        ///     <see cref="P:Turkcell.Updater.LocalizedStringMap.Item(System.String)" />
        /// </summary>
        public const String KeyImageUrl = "imageUrl";


        internal MessageDescription(String languageCode, String title, String message, String imageUrl)
            : base(languageCode, CreateMap(title, message, imageUrl))
        {
        }

        internal MessageDescription(String languageCode, JsonData jsonObject)
            : base(languageCode, jsonObject)
        {
        }

        internal MessageDescription(String languageCode, String message)
            : base(languageCode, CreateMap(message))
        {
        }

        /// <summary>
        ///     Title of the <see cref="Message" />.
        /// </summary>
        public string Title
        {
            get { return this[KeyTitle]; }
        }

        /// <summary>
        ///     Body of the <see cref="Message" />.
        /// </summary>
        public string Body
        {
            get { return this[KeyMessage]; }
        }

        /// <summary>
        ///     Url of the Image to be showed in <see cref="UpdaterDialog" />.
        /// </summary>
        public string ImageUrl
        {
            get { return this[KeyImageUrl]; }
        }

        private static Dictionary<String, String> CreateMap(String title, String message, String imageUrl)
        {
            Dictionary<String, String> result = CreateMap(message);
            result.Add(KeyTitle, title);
            result.Add(KeyImageUrl, imageUrl);
            return result;
        }

        private static Dictionary<String, String> CreateMap(String message)
        {
            var result = new Dictionary<String, String> {{KeyMessage, message}};
            return result;
        }

        public override int GetHashCode()
        {
            return (Title + Body + ImageUrl).GetHashCode();
        }
    }
}