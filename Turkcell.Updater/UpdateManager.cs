using System;
using System.Threading.Tasks;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater
{
    /// <summary>
    ///     Provides events for update check results.<br />
    ///     Conditionally one of 4 events will be fired at the end of an update check:
    ///     <ul>
    ///         <li>
    ///             <see cref="UpdateAvailable" />
    ///         </li>
    ///         <li>
    ///             <see cref="MessageAvailable" />
    ///         </li>
    ///         <li>
    ///             <see cref="NothingAvailable" />
    ///         </li>
    ///         <li>
    ///             <see cref="UpdateCheckFailed" />
    ///         </li>
    ///     </ul>
    /// </summary>
    public class UpdateManager
    {
        /// <summary>
        ///     Creates an instance of <see cref="UpdateManager" />
        /// </summary>
        public UpdateManager()
        {
            Log.PrintProductInfo();
        }

        /// <summary>
        ///     This method is called when update check is completed successfully and a newer version is found. Implementations should display {@link Update#description} to users.
        ///     Implementations should not provide an option to cancel and continue to application if {@link Update#forceUpdate} is true.
        /// </summary>
        public event EventHandler<UpdateAvailableEventArgs> UpdateAvailable;

        protected virtual void OnUpdateAvailable(UpdateAvailableEventArgs e)
        {
            EventHandler<UpdateAvailableEventArgs> handler = UpdateAvailable;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        ///     This event is fired when update check is completed successfully and a message should be displayed to user.
        ///     Implementations should not continue normal operation after this message is dismissed.
        /// </summary>
        public event EventHandler<MessageAvailableEventArgs> MessageAvailable;

        protected virtual void OnMessageAvailable(MessageAvailableEventArgs e)
        {
            EventHandler<MessageAvailableEventArgs> handler = MessageAvailable;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        ///     This event is fired when update check is completed successfully and current version is the latest version.
        /// </summary>
        public event EventHandler<EventArgs> NothingAvailable;

        protected virtual void OnNothingAvailable()
        {
            EventHandler<EventArgs> handler = NothingAvailable;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        ///     This event is fired when update check is failed.
        /// </summary>
        public event EventHandler<UpdateCheckFailedEventArgs> UpdateCheckFailed;

        protected virtual void OnUpdateCheckFailed(UpdateCheckFailedEventArgs e)
        {
            EventHandler<UpdateCheckFailedEventArgs> handler = UpdateCheckFailed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        ///     Starts an asynchronous operation for checking if a newer version of current application is available.
        /// </summary>
        /// <param name="versionServerUri">versionServerUri Location of update definitions.</param>
        /// <param name="currentProperties">currentProperties properties of current application and device.</param>
        /// <param name="postProperties">
        ///     postProperties <strong>true</strong> if current properties should post to server for server side processing.
        /// </param>
        /// <returns></returns>
        /// <exception cref="UpdaterException"></exception>
        public async Task<TurkcellUpdaterResponse> CheckUpdatesAsync(Uri versionServerUri, Properties currentProperties,
                                                                     bool postProperties)
        {
            TurkcellUpdaterResponse response;
            try
            {
                var request = new VersionMapRequest(versionServerUri, currentProperties, postProperties);
                response = await TurkcellUpdaterClient.Instance.RequestAsync(request);

                if (response.Error != null)
                    OnUpdateCheckFailed(new UpdateCheckFailedEventArgs(response.Error));

                if (response.Update != null)
                    OnUpdateAvailable(new UpdateAvailableEventArgs {Update = response.Update});

                if (response.Message != null)
                    OnMessageAvailable(new MessageAvailableEventArgs {Message = response.Message});

                if (response.Error == null && response.Update == null && response.Message == null)
                    OnNothingAvailable();
            }
            catch (Exception e) // unexpected errors.
            {
                response = new TurkcellUpdaterResponse(e);
                OnUpdateCheckFailed(new UpdateCheckFailedEventArgs(new UpdaterException(e)));
            }
            return response;
        }
    }
}