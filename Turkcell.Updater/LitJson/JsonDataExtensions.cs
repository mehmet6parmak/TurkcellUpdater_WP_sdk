using System;
using LitJson;

namespace Turkcell.Updater.LitJson
{
    internal static class JsonDataExtensions
    {
        public static String OptString(this JsonData json, string key, string defaultValue)
        {
            if (json == null || String.IsNullOrEmpty(key))
                return defaultValue;

            if (!json.ContainsKey(key))
                return defaultValue;

            return json[key].ToString();
        }

        public static String OptString(this JsonData json, string key)
        {
            return OptString(json, key, String.Empty);
        }

        public static bool OptBoolean(this JsonData json, string key)
        {
            if (json == null || String.IsNullOrEmpty(key))
                return false;

            if (!json.ContainsKey(key))
                return false;

            return bool.Parse(json[key].ToString());
        }

        public static int OptInt(this JsonData json, string key, int defaultValue)
        {
            if (json == null || String.IsNullOrEmpty(key))
                return defaultValue;

            if (!json.ContainsKey(key))
                return defaultValue;

            int result;
            if (int.TryParse(json[key].ToString(), out result))
                return result;
            return defaultValue;
        }

        public static Version OptVersion(this JsonData json, string key)
        {
            return OptVersion(json, key, new Version(0, 0, 0, 0));
        }

        public static Version OptVersion(this JsonData json, string key, Version defaultValue)
        {
            if (json == null || String.IsNullOrEmpty(key))
                return defaultValue;

            if (!json.ContainsKey(key))
                return defaultValue;

            Version result;
            if (Version.TryParse(json[key].ToString(), out result))
                return result;
            return defaultValue;
        }

        public static JsonData OptJsonData(this JsonData json, string key, JsonData defaultValue = null)
        {
            if (json == null || String.IsNullOrEmpty(key))
                return defaultValue;

            if (!json.ContainsKey(key) || (!json[key].IsObject && !json[key].IsArray))
                return defaultValue;

            return json[key];
        }
    }
}