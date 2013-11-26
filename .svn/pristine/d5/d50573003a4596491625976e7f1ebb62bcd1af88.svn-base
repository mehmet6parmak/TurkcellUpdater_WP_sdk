using System;

namespace Turkcell.Updater
{

    /// <summary>
    /// Provides information about an deployable application package 
    /// </summary>
    public class Update
    {
        /// <summary>
        /// User representable information about version.
        /// </summary>
        public UpdateDescription Description { get; private set; }

        /// <summary>
        /// Uri of .apk file. <strong>null</strong> if undefined.
        /// </summary>
        //public Uri TargetPackageUri { get; private set; }

        
        /// <summary>
        /// Uri of web page to display user to download this version. <strong>null</strong> if undefined.
        /// </summary>
        public Uri TargetWebSiteUrl { get; private set; }

        /// <summary>
        /// <strong>true</strong> if update should be performed through Windows Phone Marketplace. Not used for now.
        /// </summary>
        public bool TargetMarketplace { get; private set; }


        /// <summary>
        /// Version code of referred package.
        /// </summary>
        public Version TargetVersion { get; private set; }

        /// <summary>
        /// Package name of referred package
        /// </summary>
        public String TargetPackageId { get; internal set; }

        /// <summary>
        /// <strong>true</strong> if current application should not resume without installing this package
        /// </summary>
        public bool ForceUpdate { get; private set; }

        /// <summary>
        /// Set this property to be able to launch a target application in the case of that application is already installed on the device.
        /// Note: Only installation of the same publisher's apps are visible to current app. For 3rd party target apps SDK always launch Marketplace.
        /// </summary>
        public Uri TargetAppUriSchema { get; internal set; }

        /// <summary>
        /// If this property set to <strong>true</strong> <see cref="UpdaterDialogManager"/> will fire the <see cref="UpdaterDialogManager.ShouldExitApplication"/> event if the user does not confirm the dialog.
        /// </summary>
        public readonly bool ForceExit;


        internal Update(UpdateDescription description, bool targetMarketplace, Version targetVersionCode,
                Uri targetAppUriSchema, Uri targetWebSiteUrl, String targetPackageId, bool forceUpdate, bool forceExit)
        {
            Description = description;
            TargetMarketplace = targetMarketplace;
            TargetAppUriSchema = targetAppUriSchema;
            TargetVersion = targetVersionCode;
            TargetWebSiteUrl = targetWebSiteUrl;
            TargetPackageId = targetPackageId;
            ForceUpdate = forceUpdate;
            ForceExit = forceExit;
        }

        public override String ToString()
        {
            return "Update [description=" + Description
                //", UriSchemaToLaunchTargetApp=" + UriSchemaToLaunchTargetApp
                    + ", targetMarketplace=" + TargetMarketplace
                    + ", targetVersionCode=" + TargetVersion
                    + ", TargetPackageId=" + TargetPackageId + ", ForceUpdate="
                    + ForceUpdate + ", ForceExit=" + ForceExit + "]";
        }


    }

}
