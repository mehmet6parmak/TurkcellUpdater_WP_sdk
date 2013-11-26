using System;

namespace Turkcell.Updater
{
    /// <summary>
    /// EventArgs class containing an instance of <see cref="Message"/> class.
    /// </summary>
    public class MessageAvailableEventArgs : EventArgs
    {
        /// <summary>
        /// The <see cref="Message"/> instance available to application.
        /// </summary>
        public Message Message { get; internal set; }
    }
}