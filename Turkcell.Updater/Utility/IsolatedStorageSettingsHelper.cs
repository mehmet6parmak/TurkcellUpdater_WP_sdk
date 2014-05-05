using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace Turkcell.Updater.Utility
{
    internal class IsolatedStorageSettingsHelper
    {
        private static readonly object SyncObject = new object();

        public static ICollection Keys
        {
            get { return IsolatedStorageSettings.ApplicationSettings.Keys; }
        }

        public static void SaveItem(string key, object item, bool save = true)
        {
            IsolatedStorageSettings.ApplicationSettings[key] = item;
            if (save)
                Save();
        }

        public static void SaveItems(Dictionary<string, object> items)
        {
            foreach (var item in items)
                IsolatedStorageSettings.ApplicationSettings[item.Key] = item.Value;
            Save();
        }

        public static void Remove(string key)
        {
            IsolatedStorageSettings.ApplicationSettings.Remove(key);
            Save();
        }

        public static void Save()
        {
            lock (SyncObject)
            {
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public static T GetItem<T>(string key, T defaultValue)
        {
            T item;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out item))
                return item;
            return defaultValue;
        }

        public static T GetItem<T>(string key)
        {
            return GetItem(key, default(T));
        }
    }
}