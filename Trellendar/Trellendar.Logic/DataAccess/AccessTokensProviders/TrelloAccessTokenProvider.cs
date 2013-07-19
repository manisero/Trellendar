using Trellendar.DataAccess.Remote;

namespace Trellendar.Logic.DataAccess.AccessTokensProviders
{
    public class TrelloAccessTokenProvider : IAccessTokenProvider
    {
        private readonly UserContext _userContext;

        public bool CanProvideAccessToken
        {
            get { return _userContext.IsFilled(); }
        }

        public TrelloAccessTokenProvider(UserContext userContext)
        {
            _userContext = userContext;
        }

        public string GetAccessToken()
        {
            return _userContext.User.TrelloAccessToken;
        }
    }
}
