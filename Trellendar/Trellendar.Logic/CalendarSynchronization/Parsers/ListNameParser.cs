using System;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Parsers
{
    public class ListNameParser : ParserBase<ListName>
    {
        protected override ListName GetDefaultOutput(string text)
        {
            return new ListName { Value = text };
        }

        protected override Tuple<string, string> GetOutputTextMarkers(UserPreferences userPreferences)
        {
            return userPreferences.GetListShortcutMarkers();
        }

        protected override bool TryGetOutput(string outputText, out ListName output)
        {
            output = new ListName { Value = outputText };
            return true;
        }
    }
}
