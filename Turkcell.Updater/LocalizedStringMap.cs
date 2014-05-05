using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater
{
    /// <summary>
    ///     Provides base class for language specific set of Strings. Every String value has its own key.
    /// </summary>
    public abstract class LocalizedStringMap
    {
        /// <summary>
        ///     Two letter language code defining language of contents. <strong>null</strong> means no language is specified.
        /// </summary>
        public readonly String LanguageCode;

        private readonly Dictionary<String, String> _map;

        internal LocalizedStringMap(String languageCode, Dictionary<String, String> map)
        {
            LanguageCode = FormatLanguageCode(languageCode);

            _map = new Dictionary<String, String>(map);
        }

        internal LocalizedStringMap(String languageCode, JsonData jsonObject)
        {
            LanguageCode = FormatLanguageCode(languageCode);
            _map = new Dictionary<string, string>();
            if (jsonObject != null)
            {
                foreach (string key in jsonObject.Keys)
                {
                    if (key != null)
                    {
                        String value = jsonObject[key].ToString();
                        _map.Add(key, value);
                    }
                }
            }
        }

        /// <summary>
        ///     Returns the string value corresponding to given key.
        /// </summary>
        /// <param name="key">Key for the string value</param>
        public string this[string key]
        {
            get
            {
                if (key == null)
                {
                    return null;
                }
                return _map.ContainsKey(key) ? _map[key] : String.Empty;
            }
        }

        /// <summary>
        ///     Returns the list of available keys.
        /// </summary>
        /// <returns></returns>
        public List<String> GetKeys()
        {
            return new List<string>(_map.Keys);
        }

        private static String FormatLanguageCode(String languageCode)
        {
            languageCode = StringUtils.Normalize(languageCode);
            if (string.IsNullOrEmpty(languageCode)
                || languageCode.Equals("*"))
            {
                return null;
            }
            return languageCode;
        }


        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime*result
                     + ((LanguageCode == null) ? 0 : LanguageCode.GetHashCode());
            result = prime*result + ((_map == null) ? 0 : _map.GetHashCode());
            return result;
        }

        public override bool Equals(Object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;
            var other = (LocalizedStringMap) obj;
            if (LanguageCode == null)
            {
                if (other.LanguageCode != null)
                    return false;
            }
            else if (!LanguageCode.Equals(other.LanguageCode))
                return false;
            if (_map == null)
            {
                if (other._map != null)
                    return false;
            }
            else if (!_map.Equals(other._map))
                return false;
            return true;
        }


        internal static T Select<T>(IEnumerable<T> list, String languageCode) where T : LocalizedStringMap
        {
            String normalizedLanguageCode = StringUtils.Normalize(languageCode);
            T result = null;
            foreach (T t in list)
            {
                if (t != null)
                {
                    String normalizedLanguageCode2 = StringUtils.Normalize(t.LanguageCode);

                    if (normalizedLanguageCode2.Equals(normalizedLanguageCode))
                    {
                        result = t;
                        break;
                    }

                    if (normalizedLanguageCode2.Equals("*")
                        || normalizedLanguageCode2.Equals("")
                        || normalizedLanguageCode2
                               .Equals(normalizedLanguageCode))
                    {
                        result = t;
                    }
                }
            }
            return result;
        }


        public override String ToString()
        {
            String mapAsString;

            if (_map == null)
            {
                mapAsString = null;
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append('[');
                bool first = true;
                foreach (String key in _map.Keys)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        sb.Append(',');
                    }
                    sb.Append(key);
                    sb.Append("=");
                    sb.Append(_map[key]);
                }
                sb.Append(']');
                mapAsString = sb.ToString();
            }

            return "LocalizedStringMap [languageCode=" + LanguageCode + ", map="
                   + mapAsString + "]";
        }
    }
}