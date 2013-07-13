using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Calendar
{
    public interface ICalendarAuthorizationAPI
    {
        string GetAuthorizationUri();

        Token GetToken(string authorizationCode);

        Token GetNewToken(string refreshToken);
    }
}
