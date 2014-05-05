using Microsoft.Phone.Tasks;

namespace Turkcell.Updater.Utility
{
    internal static class MarketplaceUtility
    {
        public static void LaunchAppDetails(string packageId)
        {
            var task = new MarketplaceDetailTask
                {
                    ContentIdentifier = packageId,
                    ContentType = MarketplaceContentType.Applications
                };
            task.Show();
        }

        public static void LaunchReviewPage()
        {
            var task = new MarketplaceReviewTask();
            task.Show();
        }
    }
}