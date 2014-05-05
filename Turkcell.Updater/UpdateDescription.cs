using System;
using System.Collections.Generic;
using LitJson;

namespace Turkcell.Updater
{
    /// <summary>
    ///     Container for update message strings that are displayed in a update notification dialog.
    ///     <see cref="KeyWhatIsNew" />
    ///     <see cref="KeyMessage" />
    ///     <see cref="KeyWarnings" />
    /// </summary>
    public class UpdateDescription : LocalizedStringMap
    {
        /// <summary>
        ///     Key for summary information describing update contents.
        ///     <see cref="P:Turkcell.Updater.LocalizedStringMap.Item(System.String)" />
        /// </summary>
        internal const String KeyMessage = "message";

        /// <summary>
        ///     Key for text describing changes and new features of new version.
        /// </summary>
        internal const String KeyWhatIsNew = "whatIsNew";

        /// <summary>
        ///     Key for warning text about the update. Any important issues that user should know before updating should be described here.
        ///     <see cref="P:Turkcell.Updater.LocalizedStringMap.Item(System.String)" />
        /// </summary>
        internal const String KeyWarnings = "warnings";

        internal UpdateDescription(String languageCode, String message)
            : this(languageCode, message, null, null)
        {
        }

        internal UpdateDescription(String languageCode, String message, String whatIsNew, String warnings)
            : base(languageCode, CreateMap(message, whatIsNew, warnings))
        {
        }

        internal UpdateDescription(String languageCode, JsonData jsonObject)
            : base(languageCode, jsonObject)
        {
        }

        /// <summary>
        ///     Summary information describing update contents.
        /// </summary>
        public string Message
        {
            get { return this[KeyMessage]; }
        }

        /// <summary>
        ///     Text describing changes and new features of new version.
        /// </summary>
        public string WhatIsNew
        {
            get { return this[KeyWhatIsNew]; }
        }

        /// <summary>
        ///     Warning text about the update. Any important issues that user should know before updating should be described here.
        /// </summary>
        public string Warnings
        {
            get { return this[KeyWarnings]; }
        }

        private static Dictionary<String, String> CreateMap(String message, String whatIsNew,
                                                            String warnings)
        {
            var result = new Dictionary<String, String>
                {
                    {KeyMessage, message},
                    {KeyWarnings, warnings},
                    {KeyWhatIsNew, whatIsNew}
                };
            return result;
        }
    }
}