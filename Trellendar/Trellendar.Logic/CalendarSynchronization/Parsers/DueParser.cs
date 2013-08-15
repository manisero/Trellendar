using System;
using System.Globalization;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Parsers
{
    public class DueParser : IParser<Due>
    {
        private const string DATE_FORMAT = "yyyy-MM-dd";

        public Due Parse(string text, UserPreferences userPreferences)
        {
            if (text == null)
            {
                return null;
            }

            var dueTextMarkers = userPreferences.GetDueTextMarkers();

            if (dueTextMarkers == null || dueTextMarkers.Item1 == null || dueTextMarkers.Item2 == null)
            {
                return null;
            }

            var searchStartIndex = 0;

            while (true)
            {
                var beginningIndex = text.IndexOf(dueTextMarkers.Item1, searchStartIndex, StringComparison.Ordinal);

                if (beginningIndex < 0)
                {
                    return null;
                }

                var endIndex = text.IndexOf(dueTextMarkers.Item2, beginningIndex, StringComparison.Ordinal);

                if (endIndex < 0)
                {
                    return null;
                }

                searchStartIndex = beginningIndex + 1;

                var dueText = text.Substring(beginningIndex + dueTextMarkers.Item1.Length,
                                                    endIndex - beginningIndex - dueTextMarkers.Item1.Length);

                DateTime due;

                if (DateTime.TryParseExact(dueText, DATE_FORMAT, null, DateTimeStyles.None, out due))
                {
                    return new Due { DueDateTime = due, HasTime = false };
                }

                if (DateTime.TryParse(dueText, out due))
                {
                    return new Due { DueDateTime = due, HasTime = true };
                }
            }
        }
    }
}