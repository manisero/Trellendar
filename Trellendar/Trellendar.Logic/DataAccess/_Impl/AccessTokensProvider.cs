using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Trello;

namespace Trellendar.Logic.DataAccess._Impl
{
    public class AccessTokensProvider : ITrelloAccessTokenProvider, ICalendarAccessTokenProvider
    {
        private readonly UserContext _userContext;
        private readonly ICalendarAccessTokenExpirationHandler _calendarTokenExpirationHandler;

        public bool CanProvideTrelloAccessToken
        {
            get { return _userContext.IsFilled(); }
        }

        public bool CanProvideCalendarAccessToken
        {
            get { return _userContext.IsFilled(); }
        }

        public AccessTokensProvider(UserContext userContext, ICalendarAccessTokenExpirationHandler calendarTokenExpirationHandler)
        {
            _userContext = userContext;
            _calendarTokenExpirationHandler = calendarTokenExpirationHandler;
        }

        public string GetTrelloAccessToken()
        {
            return _userContext.User.TrelloAccessToken;
        }
        
        public string GetCalendarAccessToken()
        {
            if (_calendarTokenExpirationHandler.IsTokenExpired(_userContext.User))
            {
                _calendarTokenExpirationHandler.RefreshToken(_userContext.User);
            }

            return _userContext.User.CalendarAccessToken;
        }
    }
}
