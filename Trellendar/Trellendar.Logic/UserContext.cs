using System;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic
{
    public class UserContext
    {
        public User User { get; private set; }

        public UserContext(User user)
        {
            User = user;
        }
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

        public static UserPreferences GetUserPreferences(this UserContext userContext)
        {
            return userContext.HasUserPreferences() ? userContext.User.UserPreferences : null;
        }

        public static string GetPrefferedCardEventNameTemplate(this UserContext userContext)
        {
            return userContext.HasUserPreferences()
                       ? userContext.User.UserPreferences.CardEventNameTemplate
                       : null;
        }

        public static string GetPrefferedCheckListEventNameTemplate(this UserContext userContext)
        {
            return userContext.HasUserPreferences()
                       ? userContext.User.UserPreferences.CheckListEventNameTemplate
                       : null;
        }

        public static string GetPrefferedCheckListEventDoneSuffix(this UserContext userContext)
        {
            return userContext.HasUserPreferences()
                       ? userContext.User.UserPreferences.CheckListEventDoneSuffix
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