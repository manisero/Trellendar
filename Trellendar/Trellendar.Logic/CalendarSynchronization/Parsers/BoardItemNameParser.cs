using System;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Parsers
{
    public class BoardItemNameParser : ParserBase<BoardItemName>
    {
        protected override BoardItemName GetDefaultOutput(string text)
        {
            return new BoardItemName { Value = text };
        }

        protected override Tuple<string, string> GetOutputTextMarkers(UserPreferences userPreferences)
        {
            return userPreferences.GetBoardItemShortcutMarkers();
        }

        protected override bool TryGetOutput(string outputText, out BoardItemName output)
        {
            output = new BoardItemName { Value = outputText };
            return true;
        }
    }
}
