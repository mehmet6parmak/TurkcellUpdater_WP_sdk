using System;
using System.Globalization;
using System.Text;

namespace Turkcell.Updater.Utility
{
    internal static class StringUtils
    {
        public static string Normalize(string input)
        {
            if (input == null)
                return String.Empty;

            // CultureNames: http://msdn.microsoft.com/en-us/library/system.globalization.cultureinfo%28v=vs.80%29.ASPX
            return RemoveWhiteSpaces(input.ToLower(new CultureInfo("en-US")));
        }

        public static string RemoveWhiteSpaces(string input)
        {
            if (input == null)
            {
                return String.Empty;
            }

            int length = input.Length;
            if (length < 1)
            {
                return input;
            }

            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char c = input[i];
                if (!Char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}