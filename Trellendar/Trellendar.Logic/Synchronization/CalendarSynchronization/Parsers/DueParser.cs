using System;
using System.Globalization;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Parsers
{
    public class DueParser : ParserBase<Due>
    {
        private const string DATE_FORMAT = "yyyy-MM-dd";

        protected override Tuple<string, string> GetOutputTextMarkers(BoardCalendarBondSettings boardCalendarBondSettings)
        {
            return boardCalendarBondSettings.GetDueTextMarkers();
        }

        protected override bool TryGetOutput(string outputText, out Due output)
        {
            DateTime due;

            if (DateTime.TryParseExact(outputText, DATE_FORMAT, null, DateTimeStyles.None, out due))
            {
                output = new Due { DueDateTime = due, HasTime = false };
                return true;
            }

            if (DateTime.TryParse(outputText, out due))
            {
                output = new Due { DueDateTime = due, HasTime = true };
                return true;
            }

            output = null;
            return false;
        }
    }
}