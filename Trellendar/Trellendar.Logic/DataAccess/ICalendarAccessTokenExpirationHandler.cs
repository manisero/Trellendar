using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.DataAccess
{
    public interface ICalendarAccessTokenExpirationHandler
    {
        bool IsTokenExpired(User user);

        void RefreshToken(User user);
    }
}