using Trellendar.Domain.Calendar;
using Trellendar.Domain.Google;

namespace Trellendar.DataAccess.Remote.Google
{
    public interface IGoogleAuthorizationAPI
    {
        string GetAuthorizationUri(string redirectUri, string state = null, bool requestRefreshToken = false);

        Token GetToken(string authorizationCode, string redirectUri);

        Token GetNewToken(string refreshToken);

        UserInfo GetUserInfo(string idToken);
    }
}
