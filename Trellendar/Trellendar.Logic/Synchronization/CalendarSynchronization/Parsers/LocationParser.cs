﻿using System;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Parsers
{
    public class LocationParser : ParserBase<Location>
    {
        protected override Tuple<string, string> GetOutputTextMarkers(BoardCalendarBondSettings boardCalendarBondSettings)
        {
            return boardCalendarBondSettings.GetLocationTextMarkers();
        }

        protected override bool TryGetOutput(string outputText, out Location output)
        {
            output = new Location { Value = outputText };
            return true;
        }
    }
}
