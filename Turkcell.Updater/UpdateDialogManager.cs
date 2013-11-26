using System;
using System.Threading.Tasks;
using Turkcell.Updater.Commands;
using Turkcell.Updater.Controls;
using Turkcell.Updater.Resources;
using Turkcell.Updater.Utility;
using Windows.System;

namespace Turkcell.Updater
{

    /// <summary>
    /// Provides a mechanism for checking updates and displaying notification dialogs to user when needed.<br/>
    /// Usage example:<br/>
    /// 
    /// <pre>
    /// <code>
    /// protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
    /// {
    ///     base.OnNavigatedTo(e);
    /// 
    ///     var manager = new UpdaterDialogManager("https://dl.dropboxusercontent.com/u/218691470/Updater/message_dispcount_3.json");
    ///     manager.MessageAvailable += manager_MessageAvailable;
    ///     manager.ShouldExitApplication += manager_ShouldExitApplication;
    ///     manager.UpdateCheckCompleted += manager_UpdateCheckCompleted;
    ///     manager.UpdateCheckFailed += manager_UpdateCheckFailed;
    ///     manager.StartUpdateCheckAsync();
    /// }
    /// 
    /// void manager_UpdateCheckFailed(object sender, UpdateCheckFailedEventArgs e)
    /// {
    ///     
    /// }
    /// 
    /// void manager_UpdateCheckCompleted(object sender, System.EventArgs e)
    /// {
    ///     
    /// }
    /// 
    /// void manager_ShouldExitApplication(object sender, System.EventArgs e)
    /// {
    ///     
    /// }
    /// 
    /// void manager_MessageAvailable(object sender, DisplayMessageEventArgs e)
    /// {
    ///     
    /// }
    /// </code>
    /// </pre>
    /// 
    /// </summary>
    public class UpdaterDialogManager
    {
        //private Activity activity;
        private readonly UpdateManager _updateManager = new UpdateManager();
        //private ProgressDialog updateProgressDialog;
        private Update _update;
        //private UpdaterUiListener listener;

        /// <summary>
        /// If true a POST request will be sent to server. Otherwise GET request will be sent.
        /// </summary>
        public bool PostProperties { get; set; }
        private readonly String _updateServerUrl;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverUrl"></param>
        public UpdaterDialogManager(string serverUrl)
        {
            _updateServerUrl = serverUrl;
            _updateManager.UpdateAvailable += UpdateManagerUpdateAvailable;
            _updateManager.MessageAvailable += UpdateManagerMessageAvailable;
            _updateManager.NothingAvailable += UpdateManagerNothingAvailable;
            _updateManager.UpdateCheckFailed += UpdateManagerUpdateCheckFailed;
        }

        void UpdateManagerUpdateCheckFailed(object sender, UpdateCheckFailedEventArgs e)
        {
            OnUpdateCheckFailed(e);
        }

        void UpdateManagerNothingAvailable(object sender, EventArgs e)
        {
            OnUpdateCheckCompleted();
        }

        void UpdateManagerMessageAvailable(object sender, MessageAvailableEventArgs e)
        {
            var arguments = new DisplayMessageEventArgs(e.Message);
            OnMessageAvailable(arguments);
            if (!arguments.HandledByApplicationCode)
            {
                _activeDialog = CreateMessageDialog(e.Message);
                _activeDialog.Show();
            }
            else
            {
                OnUpdateCheckCompleted();
            }
        }

        void UpdateManagerUpdateAvailable(object sender, UpdateAvailableEventArgs e)
        {
            _update = e.Update;
            _activeDialog = new UpdaterDialog();
            if (_update.ForceExit)
            {
                _activeDialog.Title = (TextResources.Instance[TextResources.KeyServiceIsNotAvailable]);
            }
            else if (_update.ForceUpdate)
            {
                _activeDialog.Title = TextResources.Instance[TextResources.KeyUpdateRequired];
            }
            else
            {
                _activeDialog.Title = TextResources.Instance[TextResources.KeyUpdateFound];
            }

            var updateDescription = _update.Description;
            if (updateDescription != null)
            {
                var updateContent = new UpdateContent();
                _activeDialog.Content = updateContent;

                updateContent.Message = updateDescription.Message;
                updateContent.Warnings = updateDescription.Warnings;
                updateContent.WhatIsNew = updateDescription.WhatIsNew;
            }

            if (!_update.ForceExit)
            {
                _activeDialog.PositiveButtonText = GetPositiveButtonText(_update);
                _activeDialog.PositiveButtonCommand = new UpdateCommand(this, _update);
            }
            _activeDialog.NegativeButtonText = GetNegativeButtonText(_update);
            _activeDialog.NegativeButtonCommand = new UpdateNegativeCommand(_update, this);

            _activeDialog.Dismissed += DialogDismissed;
            _activeDialog.IsCancellable = (!_update.ForceExit && !_update.ForceUpdate);
            _activeDialog.Show();
        }

        /// <summary>
        /// Called when application should exit immediately. Typically this
        /// method is called in one of following conditions:
        /// <ul>
        /// <li>New version is found and ready to install. Application should be
        /// closed in order to launch new version.</li>
        /// <li>User refused to install a mandatory update. See: <see cref="Update.ForceUpdate"/>
        /// </li>
        /// </ul>
        /// </summary>
        public event EventHandler<EventArgs> ShouldExitApplication;

        protected virtual void OnShouldExitApplication()
        {
            var handler = ShouldExitApplication;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when a message should be displayed to user.<br/>
        /// This call always will be followed by
        /// <see cref="OnUpdateCheckCompleted"/> call. if this method returns <strong>true</strong>
        /// message will be handled by application and displayed to user later by
        /// calling <see cref="CreateMessageDialog"/> method<br/>
        /// 
        /// if this method returns <strong>false</strong> message will be automatically
        /// displayed to user immediately.<br/>
        /// 
        /// </summary>
        public event EventHandler<DisplayMessageEventArgs> MessageAvailable;

        protected virtual void OnMessageAvailable(DisplayMessageEventArgs e)
        {
            var handler = MessageAvailable;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<UpdateCheckFailedEventArgs> UpdateCheckFailed;

        protected virtual void OnUpdateCheckFailed(UpdateCheckFailedEventArgs e)
        {
            var handler = UpdateCheckFailed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Update check is completed. Application should continue initialization
        /// process.
        /// </summary>
        public event EventHandler<EventArgs> UpdateCheckCompleted;

        protected virtual void OnUpdateCheckCompleted()
        {
            var handler = UpdateCheckCompleted;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private UpdaterDialog _activeDialog;

        /// <summary>
        /// Checks updates and messages available asynchronously.
        /// </summary>
        /// <param name="properties">Current device and application properties.</param>
        /// <returns></returns>
        public async Task StartUpdateCheckAsync(Properties properties = null)
        {
            var versionServerUri = new Uri(_updateServerUrl);
            Properties currentProperties = properties ?? await Properties.CreateInstance();
            await _updateManager.CheckUpdatesAsync(versionServerUri, currentProperties, PostProperties);
        }

        void DialogDismissed(object sender, EventArgs e)
        {
            if (_activeDialog != null)
            {
                _activeDialog.Dismissed -= DialogDismissed;
                OnUpdateCheckCompleted();
            }
        }

        private string GetNegativeButtonText(Update update)
        {
            if (update.ForceExit || update.ForceUpdate)
                return TextResources.Instance[TextResources.KeyExitApplication];
            return TextResources.Instance[TextResources.KeyRemindMeLater];
        }

        private string GetPositiveButtonText(Update update)
        {
            string result;
            if (!PackageUtility.IsInstalled(update.TargetPackageId, update.TargetVersion))
            {
                result = TextResources.Instance[TextResources.KeyInstall];
            }
            else if (_update.TargetAppUriSchema != null)
            {
                result = TextResources.Instance[TextResources.KeyLaunch];
            }
            else
            {
                result = TextResources.Instance[TextResources.KeyInstall];
            }
            return result;
        }

        internal void FireShouldExitApplication()
        {
            OnShouldExitApplication();
        }

        /// <summary>
        /// Creates an instance of <see cref="UpdaterDialog"/> with the properties of the parameter message.
        /// </summary>
        /// <param name="message">An instance of <see cref="Message"/></param>
        /// <returns>An instance of <see cref="UpdaterDialog"/></returns>
        public UpdaterDialog CreateMessageDialog(Message message)
        {
            var dialog = new UpdaterDialog();
            InitializeMessageDialogButtons(dialog, message);
            var description = message.Description;
            dialog.Title = description.Title;
            var messageContent = new MessageContent { Message = description.Body, ImageUrl = description.ImageUrl };
            dialog.Content = messageContent;
            dialog.Dismissed += DialogDismissed;
            return dialog;
        }

        private void InitializeMessageDialogButtons(UpdaterDialog dialog, Message message)
        {

            bool viewButtonTargetGooglePlay;
            bool viewButtonEnabled = false;
            if (message != null)
            {
                if (
                    //message.TargetMarketplace && 
                    !String.IsNullOrEmpty(message.TargetPackageId))
                {
                    viewButtonTargetGooglePlay = true;
                    viewButtonEnabled = true;
                }
                else if (message.TargetWebsiteUrl != null)
                {
                    viewButtonEnabled = true;
                    viewButtonTargetGooglePlay = false;
                }
                else
                {
                    viewButtonTargetGooglePlay = false;
                }
            }
            else
            {
                viewButtonTargetGooglePlay = false;
            }
            dialog.NegativeButtonCommand = new MessageCommand(OnUpdateCheckCompleted);
            if (!viewButtonEnabled)
            {
                dialog.NegativeButtonText = TextResources.Instance[TextResources.KeyClose];
            }
            else
            {
                dialog.NegativeButtonText = TextResources.Instance[TextResources.KeyClose];
                dialog.PositiveButtonText = TextResources.Instance[TextResources.KeyView];
                dialog.PositiveButtonCommand = new MessageCommand(() =>
                    {
                        try
                        {
                            if (viewButtonTargetGooglePlay)
                            {
                                MarketplaceUtility.LaunchAppDetails(message.TargetPackageId);
                            }
                            else
                            {
                                Launcher.LaunchUriAsync(message.TargetWebsiteUrl);
                            }
                        }
                        catch (Exception err)
                        {
                            Log.E("Error On Message Dialog Action", err);
                        }
                        finally
                        {
                            OnUpdateCheckCompleted();
                        }
                    });
            }
        }

        internal void FireOnCompleted()
        {
            OnUpdateCheckCompleted();
        }
    }
}
