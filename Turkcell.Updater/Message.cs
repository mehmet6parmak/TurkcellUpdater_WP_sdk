using System;

namespace Turkcell.Updater
{
    /// <summary>
    ///     Provides information about an deployable application package
    /// </summary>
    public class Message
    {
        /// <summary>
        ///     User representable message contents.
        /// </summary>
        public readonly MessageDescription Description;

        //public readonly bool TargetMarketplace;
        /// <summary>
        ///     <strong>true</strong> if user should directed to Windows Phone Marketplace product page.
        /// </summary>
        /// <summary>
        ///     Package Id of referred package
        /// </summary>
        public readonly string TargetPackageId;

        /// <summary>
        ///     Url of web page to display user. <strong>null</strong> if undefined.
        /// </summary>
        public readonly Uri TargetWebsiteUrl;

        internal Message(MessageDescription description,
                         Uri targetWebsiteUrl,
                         //bool targetMarketplace,
                         String targetPackageId)
        {
            Description = description;
            //TargetMarketplace = targetMarketplace;
            TargetWebsiteUrl = targetWebsiteUrl;
            TargetPackageId = targetPackageId;
        }


        public override String ToString()
        {
            return "Message [description=" + Description + ", targetWebsiteUrl="
                   + TargetWebsiteUrl
                   //+ ", targetMarketplace=" + TargetMarketplace
                   + ", targetPackageId=" + TargetPackageId + "]";
        }
    }
}