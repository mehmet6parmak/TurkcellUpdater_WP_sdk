using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using LitJson;
using Turkcell.Updater.Resources;

namespace Turkcell.Updater.SampleApp
{
    public partial class MainPage
    {
        private readonly ObservableCollection<string> _logs = new ObservableCollection<string>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            TextResources.Instance[TextResources.KeyInstall] = "Yükle";
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LstUpdateOptions.ItemsSource = Updates;
            LstUpdateOptions.SelectedIndex = 0;

            LstMessageOptions.ItemsSource = Messages;
            LstMessageOptions.SelectedIndex = 0;
        }

        void _manager_UpdateCheckFailed(object sender, UpdateCheckFailedEventArgs e)
        {
            _logs.Add("Update Check Failed: " + e.Error.Message);
        }

        void _manager_UpdateCheckCompleted(object sender, EventArgs e)
        {
            _logs.Add("Update Check Completed!");
            NavigationService.Navigate(new Uri("/SecondPage.xaml", UriKind.Relative));
        }

        void _manager_ShouldExitApplication(object sender, EventArgs e)
        {
            _logs.Add("Should Exit Application: Service is not available anymore. Please uninstall the application.");
            Application.Current.Terminate();
        }
        private async void BtnCheckUpdates_OnClick(object sender, RoutedEventArgs e)
        {
            if (App.UpdateManager != null)
            {
                App.UpdateManager.MessageAvailable -= manager_MessageAvailable;
                App.UpdateManager.ShouldExitApplication -= _manager_ShouldExitApplication;
                App.UpdateManager.UpdateCheckCompleted -= _manager_UpdateCheckCompleted;
                App.UpdateManager.UpdateCheckFailed -= _manager_UpdateCheckFailed;
                App.UpdateManager = null;
            }

            if (App.UpdateManager == null)
            {
                var item = (Pivot.SelectedIndex == 0 ? LstUpdateOptions.SelectedItem : LstMessageOptions.SelectedItem) as UpdateItem;

                App.UpdateManager = new UpdaterDialogManager(item.Uri);
                App.UpdateManager.MessageAvailable += manager_MessageAvailable;
                App.UpdateManager.ShouldExitApplication += _manager_ShouldExitApplication;
                App.UpdateManager.UpdateCheckCompleted += _manager_UpdateCheckCompleted;
                App.UpdateManager.UpdateCheckFailed += _manager_UpdateCheckFailed;
                LstLog.ItemsSource = _logs;


            }

            var properties = await Properties.CreateInstance();
            App.UpdateManager.PostProperties = ChkPostProperties.IsChecked.HasValue && ChkPostProperties.IsChecked.Value;
            App.UpdateManager.StartUpdateCheckAsync(properties);
        }

        void manager_MessageAvailable(object sender, DisplayMessageEventArgs e)
        {
            _logs.Add("Message Available");
            //e.HandledByApplicationCode = true;
            //App.MessageToShow = e.Message;
            //var message = e.Message;
            //if (message != null)
            //{
            //    var dialog = _manager.CreateMessageDialog(message);
            //    dialog.Show();
            //}
        }


        public static List<UpdateItem> Updates = new List<UpdateItem>
            {
                new UpdateItem() { Title = "Optional Update", Uri = "https://dl.dropboxusercontent.com/u/18197706/Updater/optional_update.json"},
            new UpdateItem { Title = "Force Exit", Uri = "https://dl.dropboxusercontent.com/u/18197706/Updater/force_exit.json"},
            new UpdateItem { Title = "Force Update", Uri = "https://dl.dropboxusercontent.com/u/18197706/Updater/force_update.json"},
            new UpdateItem { Title = "End of support", Uri = "https://dl.dropboxusercontent.com/u/18197706/Updater/end_of_support_for_older_versions.json"}, 
            new UpdateItem { Title = "Optional - Different Target App", Uri = "https://dl.dropboxusercontent.com/u/18197706/Updater/target_mobil_asistan.json"},
            new UpdateItem {Title = "Update with Warning", Uri = "https://dl.dropboxusercontent.com/u/18197706/Updater/update_with_warning.json"},
            new UpdateItem {Title = "Multilingual self targeting update", Uri = "https://dl.dropboxusercontent.com/u/18197706/Updater/multilingual_self_targeting_update.json"},
            new UpdateItem {Title = "Launch Another App", Uri = "https://dl.dropboxusercontent.com/u/18197706/Updater/launch_another_app.json"}
        };

        public static List<UpdateItem> Messages = new List<UpdateItem>
            {
                new UpdateItem {Title = "Simple Message Dialog", Uri = "https://dl.dropboxusercontent.com/u/18197706/Updater/message_dispcount_3.json"}
            };

        public class UpdateItem
        {
            public string Title { get; set; }
            public string Uri { get; set; }
        }
    }
}