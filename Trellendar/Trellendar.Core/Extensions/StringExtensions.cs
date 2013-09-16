using System.Collections.Generic;

namespace Trellendar.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static string GetValueOrDefault(this string value)
        {
            return value.IsNotNullOrWhiteSpace() ? value : null;
        }

        public static string TrimOrDefault(this string value)
        {
            if (value == null)
            {
                return null;
            }

            return value.Trim().GetValueOrDefault();
        }

        public static string FormatWith(this string pattern, params object[] arguments)
        {
            return string.Format(pattern, arguments);
        }

        public static string JoinWith(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }
    }
}
