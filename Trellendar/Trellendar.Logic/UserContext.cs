using System;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic
{
    public class UserContext
    {
        public User User { get; set; }
    }

    public static class UserContextExtensions
    {
        public static bool IsFilled(this UserContext userContext)
        {
            return userContext != null && userContext.User != null;
        }

        public static bool HasUserPreferences(this UserContext userContext)
        {
            return userContext.IsFilled() && userContext.User.UserPreferences != null;
        }

        public static Tuple<string, string> GetPrefferedListShortcutMarkers(this UserContext userContext)
        {
            return userContext.HasUserPreferences()
                       ? Tuple.Create(userContext.User.UserPreferences.ListShortcutBeginningMarker,
                                      userContext.User.UserPreferences.ListShortcutEndMarker)
                       : null;
        }

        public static string GetPrefferedCardEventNameTemplate(this UserContext userContext)
        {
            return userContext.HasUserPreferences()
                       ? userContext.User.UserPreferences.CardEventNameTemplate
                       : null;
        }

        public static TimeSpan? GetPrefferedWholeDayEventDueTime(this UserContext userContext)
        {
            return userContext.HasUserPreferences()
                       ? userContext.User.UserPreferences.WholeDayEventDueTime
                       : null;
        }
    }
}