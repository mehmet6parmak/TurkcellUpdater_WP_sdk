using System;
using System.Collections.Generic;
using LitJson;
using Turkcell.Updater.LitJson;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater
{
    internal class MessageEntry : FilteredEntry
    {
        internal readonly DateTime? DisplayAfterDate;
        internal readonly DateTime? DisplayBeforeDate;
        internal readonly int DisplayPeriodInHours;
        internal readonly int Id;
        internal readonly int MaxDisplayCount;
        internal readonly List<MessageDescription> MessageDescriptions;
        internal readonly bool TargetMarketplace;
        internal readonly String TargetPackageId;
        internal readonly Uri TargetWebsiteUrl;

        internal MessageEntry(List<Filter> filters, int id,
                              List<MessageDescription> messageDescriptions,
                              int displayPeriodInHours, DateTime displayAfterDate,
                              DateTime displayBeforeDate, int maxDisplayCount,
                              String targetPackageName,
                              Uri targetWebsiteUrl, bool targetMarketplace)
            : base(filters)
        {
            MessageDescriptions = messageDescriptions;
            DisplayPeriodInHours = displayPeriodInHours;
            DisplayAfterDate = displayAfterDate;
            DisplayBeforeDate = displayBeforeDate;
            MaxDisplayCount = maxDisplayCount;
            TargetPackageId = targetPackageName;
            TargetMarketplace = targetMarketplace;
            TargetWebsiteUrl = targetWebsiteUrl;
            Id = id == 0 ? GenerateId() : id;

            Validate();
        }

        internal MessageEntry(JsonData jsonObject)
            : base(jsonObject)
        {
            MessageDescriptions = CreateMessageDescriptions(jsonObject);
            DisplayPeriodInHours = jsonObject.OptInt("displayPeriodInHours", 0);
            DisplayAfterDate = GetDate(jsonObject, "displayAfterDate");
            DisplayBeforeDate = GetDate(jsonObject, "displayBeforeDate");
            MaxDisplayCount = jsonObject.OptInt("maxDisplayCount", int.MaxValue);

            TargetWebsiteUrl = GetUrl(jsonObject, "targetWebsiteUrl");

            TargetPackageId = StringUtils.RemoveWhiteSpaces(jsonObject.OptString("targetPackageName"));

            TargetMarketplace = jsonObject.OptBoolean("targetGooglePlay");

            int i = jsonObject.OptInt("id", 0);
            Id = i == 0 ? GenerateId() : i;

            Validate();
        }

        private int GenerateId()
        {
            const int prime = 31;
            int result = 1;
            result = prime*result + (TargetMarketplace ? 1231 : 1237);
            result = prime
                     *result
                     + ((TargetPackageId == null) ? 0 : TargetPackageId.GetHashCode());
            result = prime
                     *result
                     + ((TargetWebsiteUrl == null) ? 0 : TargetWebsiteUrl.GetHashCode());


            int hc = 0;
            if (MessageDescriptions != null)
                foreach (MessageDescription p in MessageDescriptions)
                    hc ^= p.GetHashCode();

            result = prime
                     *result
                     + ((MessageDescriptions == null) ? 0 : hc);
            return result;
        }

        private static Uri GetUrl(JsonData jsonObject, String key)
        {
            String spec = StringUtils.RemoveWhiteSpaces((jsonObject.OptString(key)));
            if ("".Equals(spec))
            {
                return null;
            }

            try
            {
                return new Uri(spec);
            }
            catch (UriFormatException e)
            {
                throw new UpdaterException("'" + key + "' url is malformatted", e);
            }
        }

        private static DateTime? GetDate(JsonData jsonObject, String name)
        {
            if (jsonObject == null || name == null)
            {
                return null;
            }

            String s = jsonObject.OptString(name, null);
            if (s == null)
            {
                return null;
            }
            return DateTimeUtils.ParseIsoDate(s);
        }

        private static List<MessageDescription> CreateMessageDescriptions(JsonData jsonObject)
        {
            var result = new List<MessageDescription>();
            JsonData udsObject = jsonObject.OptJsonData("descriptions");

            if (udsObject != null)
            {
                foreach (string key in udsObject.Keys)
                {
                    string languageCode = key;
                    JsonData o = udsObject.OptJsonData(languageCode);
                    if (o != null)
                    {
                        var ud = new MessageDescription(languageCode, o);
                        result.Add(ud);
                    }
                    else
                    {
                        string s = udsObject.OptString(languageCode, null);
                        if (s != null)
                        {
                            var ud = new MessageDescription(languageCode, s);
                            result.Add(ud);
                        }
                    }
                }
            }

            return result;
        }

        internal bool ShouldDisplay(Properties properties, MessageDisplayRecords records)
        {
            // ReSharper disable CSharpWarnings::CS0612
            return ShouldDisplay(properties, records, DateTime.Now);
            // ReSharper restore CSharpWarnings::CS0612
        }

        /// <summary>
        ///     Test friendly version of
        ///     <see
        ///         cref="ShouldDisplay(Turkcell.Updater.Properties,Turkcell.Updater.MessageDisplayRecords)" />
        ///     <br />
        ///     <em>
        ///         <strong>Note:</strong>
        ///     </em>
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="records"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        [Obsolete("This method is should only be used for testing purposes.")]
        internal bool ShouldDisplay(Properties properties, MessageDisplayRecords records, DateTime now)
        {
            // check filters
            if (!IsMatches(properties))
            {
                return false;
            }

            // check if it is early to display message
            if (DisplayAfterDate != null)
            {
                if (DisplayAfterDate >= (now))
                {
                    return false;
                }
            }

            // check if it is late to display message
            if (DisplayBeforeDate != null)
            {
                if (DisplayBeforeDate <= (now))
                {
                    return false;
                }
            }

            // check if it is displayed more than specified count
            if (MaxDisplayCount < int.MaxValue)
            {
                int count = records.GetMessageDisplayCount(Id);
                if (count >= MaxDisplayCount)
                {
                    return false;
                }
            }

            // check if it is displayed earlier than specified period
            if (DisplayPeriodInHours > 0)
            {
                DateTime? messageLastDisplayDate = records.GetMessageLastDisplayDate(Id);

                // check if message displayed before
                if (messageLastDisplayDate.HasValue)
                {
                    DateTime date = now.AddHours(-DisplayPeriodInHours);
                    if (messageLastDisplayDate > (date))
                    {
                        return false;
                    }
                }
            }

            // check if target application is already installed
            if (!String.IsNullOrEmpty(TargetPackageId))
            {
                //Check Windows.Phone.Management.Deployment.InstallationManager               
                if (PackageUtility.IsInstalled(TargetPackageId))
                {
                    return false;
                }
            }

            return true;
        }

        internal Message GetMessageToDisplay(Properties properties, MessageDisplayRecords records)
        {
            // ReSharper disable CSharpWarnings::CS0612
            return GetMessageToDisplay(properties, records, DateTime.Now);
            // ReSharper restore CSharpWarnings::CS0612
        }

        /// <summary>
        ///     Test friendly version of
        ///     <see
        ///         cref="GetMessageToDisplay(Turkcell.Updater.Properties,Turkcell.Updater.MessageDisplayRecords)" />
        ///     <br />
        ///     <em>
        ///         <strong>Note:</strong> This method is should only be used for testing purposes.
        ///     </em>
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="records"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        [Obsolete("This method is should only be used for testing purposes.")]
        internal Message GetMessageToDisplay(Properties properties, MessageDisplayRecords records, DateTime now)
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

            MessageDescription description = LocalizedStringMap.Select(MessageDescriptions, languageCode);
            records.OnMessageDisplayed(Id, now);

            return new Message(description, TargetWebsiteUrl
                               //, TargetMarketplace
                               , TargetPackageId);
        }

        private void Validate()
        {
            if (TargetMarketplace && String.IsNullOrEmpty(TargetPackageId))
            {
                throw new UpdaterException(
                    "'targetPackageName' shoud be not be empty if target is Marketplace");
            }
        }
    }
}