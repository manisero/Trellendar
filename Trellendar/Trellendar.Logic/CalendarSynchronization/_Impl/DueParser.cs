using System;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class DueParser : IDueParser
    {
        public DateTime? Parse(string textWithDue)
        {
            if (textWithDue == null)
            {
                return null;
            }

            var beginningIndex = textWithDue.IndexOf("[", StringComparison.Ordinal);

            if (beginningIndex < 0)
            {
                return null;
            }

            var endIndex = textWithDue.IndexOf("]", beginningIndex, StringComparison.Ordinal);

            if (endIndex < 0)
            {
                return null;
            }

            var dueText = textWithDue.Substring(beginningIndex + "[".Length,
                                                endIndex - beginningIndex - "]".Length);

            DateTime due;

            return DateTime.TryParse(dueText, out due) ? due : (DateTime?)null;
        }
    }
}