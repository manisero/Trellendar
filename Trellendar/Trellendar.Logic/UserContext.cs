using Trellendar.Domain.Native;

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
    }
}