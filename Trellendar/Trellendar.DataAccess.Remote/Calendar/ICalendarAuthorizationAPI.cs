using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Remote.Calendar
{
    public interface ICalendarAuthorizationAPI
    {
        string GetAuthorizationUri(string redirectUri);

        Token GetToken(string authorizationCode, string redirectUri);

        Token GetNewToken(string refreshToken);

        UserInfo GetUserInfo(string idToken);
    }
}
