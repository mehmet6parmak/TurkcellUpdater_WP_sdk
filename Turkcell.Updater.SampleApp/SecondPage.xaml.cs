using System.Windows;
using Microsoft.Phone.Controls;

namespace Turkcell.Updater.SampleApp
{
    public partial class SecondPage : PhoneApplicationPage
    {
        public SecondPage()
        {
            InitializeComponent();
            Loaded += SecondPage_Loaded;
        }

        void SecondPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.MessageToShow != null)
            {
                var messageDialog = App.UpdateManager.CreateMessageDialog(App.MessageToShow);
                messageDialog.Show();                
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var manager = new UpdaterDialogManager("https://dl.dropboxusercontent.com/u/218691470/Updater/message_dispcount_3.json");
            manager.MessageAvailable += manager_MessageAvailable;
            manager.ShouldExitApplication += manager_ShouldExitApplication;
            manager.UpdateCheckCompleted += manager_UpdateCheckCompleted;
            manager.UpdateCheckFailed += manager_UpdateCheckFailed;
            //manager.StartUpdateCheckAsync();
        }

        void manager_UpdateCheckFailed(object sender, UpdateCheckFailedEventArgs e)
        {
            
        }

        void manager_UpdateCheckCompleted(object sender, System.EventArgs e)
        {
            
        }

        void manager_ShouldExitApplication(object sender, System.EventArgs e)
        {
            
        }

        void manager_MessageAvailable(object sender, DisplayMessageEventArgs e)
        {
            
        }
    }
}