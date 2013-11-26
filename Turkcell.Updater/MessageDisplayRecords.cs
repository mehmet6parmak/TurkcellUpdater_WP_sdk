using System;
using System.Collections.Generic;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater
{
    /// <summary>
    /// Keeps information about displayed messages like last display date and display count.
    /// <br/>
    /// Data kept by this class is persisted in IsolatedStorageSettings with key prefix "turkcell-updater-". 
    /// </summary>
    internal class MessageDisplayRecords
    {
        internal const String IsolatedStorageKeyPrefix = "turkcell-updater-";

        /// <summary>
        /// Returns display count of message with given <strong>id</strong>
        /// </summary>
        /// <param name="id">ID of queried message</param>
        /// <returns>total display count.</returns>
        internal int GetMessageDisplayCount(int id)
        {
            return IsolatedStorageSettingsHelper.GetItem<int>(IsolatedStorageKeyPrefix + id + "-display-count");
        }

        /// <summary>
        /// Returns last display time of message with given <strong>id</strong>
        /// </summary>
        /// <param name="id">ID of queried message</param>
        /// <returns>last display date or <strong>null</strong> if message is not displayed yet.</returns>
        internal DateTime? GetMessageLastDisplayDate(int id)
        {
            var displayDate = IsolatedStorageSettingsHelper.GetItem<DateTime?>(IsolatedStorageKeyPrefix + id + "-last-display-date", null);
            if (!displayDate.HasValue)
            {
                return null;
            }
            return displayDate;
        }

        /// <summary>
        /// Increases display count of message with given <strong>id</strong> by one and stores current time as last display date for the message.
        /// </summary>
        /// <param name="id">ID of queried message</param>
        internal void OnMessageDisplayed(int id)
        {
            // ReSharper disable CSharpWarnings::CS0612
            OnMessageDisplayed(id, DateTime.Now);
            // ReSharper restore CSharpWarnings::CS0612
        }

        /// <summary>
        /// Test friendly version of <see cref="OnMessageDisplayed(int)"/>.
        /// <br/>
        /// <em><strong>Note:</strong> This method is should only be used for testing purposes.</em>
        /// </summary>
        /// <param name="id">ID of the queried message</param>
        /// <param name="displayDate">DateTime to set as displayDate</param>
        [Obsolete("This method is should only be used for testing purposes.")]
        internal void OnMessageDisplayed(int id, DateTime displayDate)
        {
            int messageDisplayCount = GetMessageDisplayCount(id);
            IsolatedStorageSettingsHelper.SaveItem(IsolatedStorageKeyPrefix + id + "-display-count", messageDisplayCount + 1, false);
            IsolatedStorageSettingsHelper.SaveItem(IsolatedStorageKeyPrefix + id + "-last-display-date", displayDate);
        }

        /// <summary>
        /// Deletes record for given id.
        /// <br/>
        /// <em><strong>Note:</strong> This method is should only be used for testing purposes.</em>
        /// </summary>
        /// <param name="id">ID of the queried message</param>
        [Obsolete("This method is should only be used for testing purposes.")]
        internal void DeleteMessageRecords(int id)
        {
            IsolatedStorageSettingsHelper.Remove(IsolatedStorageKeyPrefix + id + "-display-count");
            IsolatedStorageSettingsHelper.Remove(IsolatedStorageKeyPrefix + id + "-last-display-date");
        }

        /// <summary>
        /// Deletes record for given id.
        /// <br/>
        /// <em><strong>Note:</strong> This method is should only be used for testing purposes.</em>
        /// </summary>
        [Obsolete("This method is should only be used for testing purposes.")]
        internal void DeleteMessageRecords()
        {
            var keysList = new List<string>();

            foreach (string key in IsolatedStorageSettingsHelper.Keys)
            {
                if (key.StartsWith(IsolatedStorageKeyPrefix))
                    keysList.Add(key);
            }

            foreach (var key in keysList)
            {
                IsolatedStorageSettingsHelper.Remove(key);
            }
        }
    }
}
