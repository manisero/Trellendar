using System;
using System.Globalization;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class DueParser : IDueParser
    {
        private const string DATE_FORMAT = "yyyy-MM-dd";

        private readonly UserContext _userContext;

        public DueParser(UserContext userContext)
        {
            _userContext = userContext;
        }

        public Due Parse(string textWithDue)
        {
            if (textWithDue == null)
            {
                return null;
            }

            var dueTextMarkers = _userContext.GetPrefferedDueTextMarkers();

            if (dueTextMarkers == null || dueTextMarkers.Item1 == null || dueTextMarkers.Item2 == null)
            {
                return null;
            }

            var searchStartIndex = 0;

            while (true)
            {
                var beginningIndex = textWithDue.IndexOf(dueTextMarkers.Item1, searchStartIndex, StringComparison.Ordinal);

                if (beginningIndex < 0)
                {
                    return null;
                }

                var endIndex = textWithDue.IndexOf(dueTextMarkers.Item2, beginningIndex, StringComparison.Ordinal);

                if (endIndex < 0)
                {
                    return null;
                }

                searchStartIndex = beginningIndex + 1;

                var dueText = textWithDue.Substring(beginningIndex + dueTextMarkers.Item1.Length,
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