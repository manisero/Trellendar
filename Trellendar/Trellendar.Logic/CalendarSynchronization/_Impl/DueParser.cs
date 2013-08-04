using System;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class DueParser : IDueParser
    {
        private readonly UserContext _userContext;

        public DueParser(UserContext userContext)
        {
            _userContext = userContext;
        }

        public DateTime? Parse(string textWithDue)
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

            var beginningIndex = textWithDue.IndexOf(dueTextMarkers.Item1, StringComparison.Ordinal);

            if (beginningIndex < 0)
            {
                return null;
            }

            var endIndex = textWithDue.IndexOf(dueTextMarkers.Item2, beginningIndex, StringComparison.Ordinal);

            if (endIndex < 0)
            {
                return null;
            }

            var dueText = textWithDue.Substring(beginningIndex + dueTextMarkers.Item1.Length,
                                                endIndex - beginningIndex - dueTextMarkers.Item2.Length);

            DateTime due;

            return DateTime.TryParse(dueText, out due) ? due : (DateTime?)null;
        }
    }
}