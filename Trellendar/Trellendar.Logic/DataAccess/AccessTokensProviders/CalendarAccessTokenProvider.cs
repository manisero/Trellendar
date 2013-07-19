using Trellendar.DataAccess.Remote;

namespace Trellendar.Logic.DataAccess.AccessTokensProviders
{
    public class CalendarAccessTokenProvider : IAccessTokenProvider
    {
        private readonly UserContext _userContext;
        private readonly ICalendarAccessTokenExpirationHandler _tokenExpirationHandler;

        public bool CanProvideAccessToken
        {
            get { return _userContext.IsFilled(); }
        }

        public CalendarAccessTokenProvider(UserContext userContext, ICalendarAccessTokenExpirationHandler tokenExpirationHandler)
        {
            _userContext = userContext;
            _tokenExpirationHandler = tokenExpirationHandler;
        }

        public string GetAccessToken()
        {
            if (_tokenExpirationHandler.IsTokenExpired(_userContext.User))
            {
                _tokenExpirationHandler.RefreshToken(_userContext.User);
            }

            return _userContext.User.CalendarAccessToken;
        }
    }
}
