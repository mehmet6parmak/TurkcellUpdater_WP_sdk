using System;

namespace Turkcell.Updater
{
    /// <summary>
    /// Class containing DisplayMessage arguments. 
    /// </summary>
    public class DisplayMessageEventArgs : EventArgs
    {
        /// <summary>
        /// <see cref="Message"/> instance to be shown.
        /// </summary>
        public Message Message { get; internal set; }

        /// <summary>
        /// If this property is set to <strong>true</strong> <see cref="DisplayMessageEventArgs.Message"/> will be handled by application and displayed to the user later by calling <see cref="UpdaterDialogManager.CreateMessageDialog"/>.
        /// <br/>
        /// If <strong>false</strong> <see cref="DisplayMessageEventArgs.Message"/> will be automatically displayed to the user immediately.
        /// </summary>
        public bool HandledByApplicationCode { get; set; }

        internal DisplayMessageEventArgs(Message message)
        {
            Message = message;
        }
    }
}