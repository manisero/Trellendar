using System.Collections.Generic;

namespace Trellendar.Core.Extensions
{
    public static class StringExtensions
    {
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
