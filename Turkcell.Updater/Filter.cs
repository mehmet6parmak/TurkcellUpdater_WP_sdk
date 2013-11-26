using System;
using System.Text.RegularExpressions;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater
{
    /// <summary>
    /// Checks if given value matches with filtering rule.<br/>
    /// <li>Rules are sequences of rule parts joined with ","</li>
    /// <li>Both rule parts and values are converted to lower case and trimmed before
    /// comparison</li>
    /// <li>Order of rule parts doesn't change the result, example: "!b,a" is same with "a,!b"</li>
    /// <li><strong>"*"</strong>, <strong>null</strong> or empty string matches with any value including
    /// <strong>null</strong></li>
    /// <li><strong>"''"</strong> matches with <strong>null</strong> or empty string</li>
    /// <li><strong>"!''"</strong> matches with any value except <strong>null</strong> or empty string</li>
    /// <li><strong>"![rule part]"</strong> excludes any value matches with [rule].</li>
    /// <li><strong>"[value]"</strong> matches with any value equals to [value]</li>
    /// <li><strong>"[prefix]*"</strong> matches with any value starting with [prefix]</li>
    /// <li><strong>"*[suffix]"</strong> matches with any value ending with [suffix]</li>
    /// <li><strong>"[prefix]*[suffix]"</strong> matches with any value starting with [prefix] and
    /// ending with [suffix]</li>
    /// <li><strong>"&gt;[integer]"</strong> matches with any value greater than [integer]</li>
    /// <li><strong>"&gt;=[integer]"</strong> matches with any value greater than or equals to [integer]</li>
    /// <li><strong>"&lt;[integer]"</strong> matches with any value lesser than [integer]</li>
    /// <li><strong>"&lt;=[integer]"</strong> matches with any value lesser than or equals to [integer]</li>
    /// <li><strong>"&lt;&gt;[integer]"</strong> matches with any value not equals to [integer]</li>
    /// </summary>
    public class Filter
    {
        internal string Name { get { return _name; } }
        readonly String _name;
        readonly String _rule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rule"></param>
        public Filter(String name, String rule)
        {
            _name = name;
            _rule = rule;
        }

        internal bool IsMatchesWith(String value)
        {
            return IsMatchesWith(value, _rule);
        }

        private static bool IsMatchesWith(String value, String rule)
        {
            value = StringUtils.Normalize(value);
            if (String.IsNullOrEmpty(rule))
            {
                return true;
            }

            // should match with any value excluding filtered ones if rule has no include filters
            // example: "a" should match with "!b,!c" rule
            bool onlyExcludeFiltersFound = true;

            // since exclude filters has higher priority over include filters,
            // we should not immediately return true when value matches with an include filter.
            // example: "abc" should not match with "a*c,!*b*" rule
            bool matchedWithAnIncludeFilter = false;

            String[] ruleParts = rule.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ruleParts.Length; i++)
            {
                String part = StringUtils.Normalize(ruleParts[i]);
                if (part.Length < 1)
                {
                    // omit empty rules
                    continue;
                }

                if (part.StartsWith("!"))
                {
                    // Exclude rule
                    if (part.Length > 1)
                    {
                        // omit empty rules
                        if (IsFilterPartMatches(part.Substring(1), value))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    // Include rule
                    onlyExcludeFiltersFound = false;
                    if (!matchedWithAnIncludeFilter)
                    {
                        if (IsFilterPartMatches(part, value))
                        {
                            matchedWithAnIncludeFilter = true;
                        }
                    }
                }

            }

            return onlyExcludeFiltersFound || matchedWithAnIncludeFilter;
        }

        private static bool IsFilterPartMatches(String rulePart, String value)
        {
            if (rulePart.Equals("''"))
            {
                return value.Equals("");
            }
            if (rulePart.StartsWith("<>"))
            {
                Version valueAsVersion;
                Version referenceVersion;
                if (Version.TryParse(value, out valueAsVersion) && Version.TryParse(rulePart.Substring(2).Trim(), out referenceVersion))
                {
                    return valueAsVersion != referenceVersion;
                }

                int valueAsint;
                int reference;
                if (int.TryParse(value, out valueAsint) && int.TryParse(rulePart.Substring(2).Trim(), out reference))
                {
                    return valueAsint != reference;
                }
                return false;
            }
            if (rulePart.StartsWith("<="))
            {
                Version valueAsVersion;
                Version referenceVersion;
                if (Version.TryParse(value, out valueAsVersion) && Version.TryParse(rulePart.Substring(2).Trim(), out referenceVersion))
                {
                    return valueAsVersion <= referenceVersion;
                }
                
                int valueAsint;
                int reference;
                if (int.TryParse(value, out valueAsint) && int.TryParse(rulePart.Substring(2).Trim(), out reference))
                {
                    return valueAsint <= reference;
                }
                return false;
            }
            if (rulePart.StartsWith(">="))
            {
                Version valueAsVersion;
                Version referenceVersion;
                if (Version.TryParse(value, out valueAsVersion) && Version.TryParse(rulePart.Substring(2).Trim(), out referenceVersion))
                {
                    return valueAsVersion >= referenceVersion;
                }

                int valueAsint;
                int reference;
                if (int.TryParse(value, out valueAsint) && int.TryParse(rulePart.Substring(2).Trim(), out reference))
                {
                    return valueAsint >= reference;
                }
                return false;
            }
            if (rulePart.StartsWith("<"))
            {
                Version valueAsVersion;
                Version referenceVersion;
                if (Version.TryParse(value, out valueAsVersion) && Version.TryParse(rulePart.Substring(1).Trim(), out referenceVersion))
                {
                    return valueAsVersion < referenceVersion;
                }

                int valueAsint;
                int reference;
                if (int.TryParse(value, out valueAsint) && int.TryParse(rulePart.Substring(1).Trim(), out reference))
                {

                    return valueAsint < reference;
                }
                return false;
            }
            if (rulePart.StartsWith(">"))
            {
                Version valueAsVersion;
                Version referenceVersion;
                if (Version.TryParse(value, out valueAsVersion) && Version.TryParse(rulePart.Substring(1).Trim(), out referenceVersion))
                {
                    return valueAsVersion > referenceVersion;
                }

                int valueAsint;
                int reference;
                if (int.TryParse(value, out valueAsint) && int.TryParse(rulePart.Substring(1).Trim(), out reference))
                {
                    return valueAsint > reference;
                }
                return false;
            }

            if (rulePart.IndexOf("*", StringComparison.Ordinal) > -1)
            {
                String regex = rulePart.Replace("?", ".").Replace("*", ".*");
                if (!regex.StartsWith("^"))
                    regex = "^" + regex;
                if (!regex.EndsWith("$"))
                    regex = regex + "$";

                var reg = new Regex(regex, RegexOptions.Singleline);
                return reg.IsMatch(value);
                //return value.matches(regex);
            }

            return rulePart.Equals(value);
        }

    }
}
