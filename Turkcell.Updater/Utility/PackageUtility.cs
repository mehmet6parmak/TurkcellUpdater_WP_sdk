using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Phone.Management.Deployment;

namespace Turkcell.Updater.Utility
{
    internal static class PackageUtility
    {
        public static bool IsInstalled(string packageId, Version targetVersion = null)
        {
            if (String.IsNullOrEmpty(packageId))
                return false;

            IEnumerable<Package> packages = InstallationManager.FindPackagesForCurrentPublisher();
            Package package = null;
            if (targetVersion == null)
                package =
                    packages.SingleOrDefault(
                        p => p.Id.ProductId.Equals(packageId, StringComparison.InvariantCultureIgnoreCase));
            else
                package =
                    packages.SingleOrDefault(
                        p => p.Id.ProductId.Equals(packageId, StringComparison.InvariantCultureIgnoreCase)
                             && p.Id.Version.Major == targetVersion.Major && p.Id.Version.Minor == targetVersion.Minor);
            return package != null;
        }
    }
}