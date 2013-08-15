using System;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.Domain
{
    public static class UserPreferencesExtensions
    {
        public static Tuple<string, string> GetDueTextMarkers(this UserPreferences userPreferences)
        {
            return userPreferences != null
                       ? Tuple.Create(userPreferences.DueTextBeginningMarker,
                                      userPreferences.DueTextEndMarker)
                       : null;
        }
    }
}
