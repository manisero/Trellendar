using System;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.Domain
{
    public static class BoardCalendarBondSettingsExtensions
    {
        public static Tuple<string, string> GetBoardItemShortcutMarkers(this BoardCalendarBondSettings settings)
        {
            return settings != null
                       ? Tuple.Create(settings.TrelloItemShortcutBeginningMarker,
                                      settings.TrelloItemShortcutEndMarker)
                       : null;
        }

        public static Tuple<string, string> GetDueTextMarkers(this BoardCalendarBondSettings settings)
        {
            return settings != null
                       ? Tuple.Create(settings.DueTextBeginningMarker,
                                      settings.DueTextEndMarker)
                       : null;
        }

        public static Tuple<string, string> GetLocationTextMarkers(this BoardCalendarBondSettings settings)
        {
            return settings != null
                       ? Tuple.Create(settings.LocationTextBeginningMarker,
                                      settings.LocationTextEndMarker)
                       : null;
        }
    }
}
