using System;

namespace Turkcell.Updater
{

    /// <summary>
    /// Provides information about an deployable application package
    /// </summary>
    public class Message
    {

        /// <summary>
        /// User representable message contents.
        /// </summary>
        public readonly MessageDescription Description;

        /// <summary>
        /// Url of web page to display user. <strong>null</strong> if undefined.
        /// </summary>
        public readonly Uri TargetWebsiteUrl;

        /// <summary>
        /// <strong>true</strong> if user should directed to Windows Phone Marketplace product page.
        /// </summary>
        //public readonly bool TargetMarketplace;

        /// <summary>
        /// Package Id of referred package
        /// </summary>
        public readonly string TargetPackageId;

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
