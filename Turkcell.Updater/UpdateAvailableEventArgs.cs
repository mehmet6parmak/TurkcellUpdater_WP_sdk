using System;

namespace Turkcell.Updater
{
    /// <summary>
    /// Passed as argument to <see cref="UpdateManager.UpdateAvailable"/> handlers.
    /// </summary>
    public class UpdateAvailableEventArgs : EventArgs
    {
        /// <summary>
        /// <see cref="Update"/> instance available to current application.
        /// </summary>
        public Update Update { get; internal set; }
    }
}