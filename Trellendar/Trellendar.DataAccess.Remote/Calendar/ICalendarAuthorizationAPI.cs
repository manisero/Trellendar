using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Remote.Calendar
{
    public interface ICalendarAuthorizationAPI
    {
        string GetAuthorizationUri();

        Token GetToken(string authorizationCode);

        Token GetNewToken(string refreshToken);

        UserInfo GetUserInfo(string idToken);
    }
}
