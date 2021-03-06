﻿using Trellendar.Domain.Trellendar;

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
    }
}