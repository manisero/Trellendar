using System;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Parsers
{
    public class LocationParser : ParserBase<Location>
    {
        protected override Tuple<string, string> GetOutputTextMarkers(UserPreferences userPreferences)
        {
            return userPreferences.GetLocationTextMarkers();
        }

        protected override bool TryGetOutput(string outputText, out Location output)
        {
            output = new Location { Value = outputText };
            return true;
        }
    }
}
