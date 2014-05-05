using System;

namespace Turkcell.Updater
{
    /// <summary>
    ///     Class containing update check failure reasons.
    /// </summary>
    public class UpdateCheckFailedEventArgs : EventArgs
    {
        internal UpdateCheckFailedEventArgs(UpdaterException err)
        {
            Error = err;
        }

        /// <summary>
        ///     Exception object describing the root cause of the problem.
        /// </summary>
        public UpdaterException Error { get; private set; }
    }
}