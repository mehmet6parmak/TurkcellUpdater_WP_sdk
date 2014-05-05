using System;
using System.Globalization;

namespace Turkcell.Updater.Utility
{
    internal static class DateTimeUtils
    {
        private static readonly CultureInfo EnUs = new CultureInfo("en-US");

        public static DateTime? ParseIsoDate(String input)
        {
            var formats = new[]
                {
                    "yyyy-MM-dd'T'HH:mm:ss.fffZ",
                    "yyyy-MM-dd HH:mm:ss.fffZ", "yyyy-MM-dd HH:mm:ss.ff", "yyyy-MM-dd HH:mm:ss.ffZ",
                    "yyyy-MM-dd HH:mm:ssZ",
                    "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mmZ", "yyyy-MM-dd HH:mm",
                    "yyyy-MM-dd"
                };

            DateTime result;
            if (DateTime.TryParseExact(input, formats, EnUs, DateTimeStyles.None, out result))
                return result;
            return null;
        }
    }
}