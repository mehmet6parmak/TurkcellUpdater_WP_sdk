using System;
using System.Collections.Generic;
using LitJson;

namespace Turkcell.Updater
{

    /// <summary>
    /// Base class for information that can be filtered by a list of <see cref="Filter"/>s
    /// </summary>
    internal class FilteredEntry
    {
        private readonly List<Filter> _filters;

        internal FilteredEntry(List<Filter> filters)
        {
            _filters = filters;
        }

        internal FilteredEntry(JsonData jsonObject)
        {
            _filters = CreateFilters(jsonObject);
        }

        private static List<Filter> CreateFilters(JsonData jsonObject)
        {
            var result = new List<Filter>();
            JsonData filtersObject = null;
            if (jsonObject.ContainsKey("filters"))
                filtersObject = jsonObject["filters"];


            if (filtersObject != null)
            {
                foreach (var key in filtersObject.Keys)
                {
                    var rule = filtersObject[key].ToString();
                    var filter = new Filter(key, rule);
                    result.Add(filter);
                }
            }

            return result;
        }

        internal bool IsMatches(Properties properties)
        {
            if (_filters != null)
            {

                foreach (Filter filter in _filters)
                {
                    if (filter != null)
                    {
                        if (properties == null)
                        {
                            return false;
                        }

                        String value = properties[filter.Name];

                        if (!filter.IsMatchesWith(value))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }

}
