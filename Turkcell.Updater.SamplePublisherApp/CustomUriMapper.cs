using System;
using System.Windows.Navigation;

namespace Turkcell.Updater.SamplePublisherApp
{
    public class CustomUriMapper : UriMapperBase
    {
        public override Uri MapUri(Uri uri)
        {
            var uriStr = System.Net.HttpUtility.UrlDecode(uri.ToString());

            if (uriStr.Contains("helloworld"))
                return new Uri("/SecondPage.xaml", UriKind.Relative);
            return uri;
        }
    }
}
