using System;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.Domain
{
    public static class UserPreferencesExtensions
    {
        public static Tuple<string, string> GetListShortcutMarkers(this UserPreferences userPreferences)
        {
            return userPreferences != null
                       ? Tuple.Create(userPreferences.ListShortcutBeginningMarker,
                                      userPreferences.ListShortcutEndMarker)
                       : null;
        }

        public static Tuple<string, string> GetDueTextMarkers(this UserPreferences userPreferences)
        {
            return userPreferences != null
                       ? Tuple.Create(userPreferences.DueTextBeginningMarker,
                                      userPreferences.DueTextEndMarker)
                       : null;
        }

        public static Tuple<string, string> GetLocationTextMarkers(this UserPreferences userPreferences)
        {
            return userPreferences != null
                       ? Tuple.Create(userPreferences.LocationTextBeginningMarker,
                                      userPreferences.LocationTextEndMarker)
                       : null;
        }
    }
}
