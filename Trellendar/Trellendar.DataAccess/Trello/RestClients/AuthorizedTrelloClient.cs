using System.Collections.Generic;

namespace Trellendar.DataAccess.Trello.RestClients
{
    public class AuthorizedTrelloClient : TrelloClient
    {
        private readonly ITrelloAccessTokenProvider _tokenProvider;

        public AuthorizedTrelloClient(ITrelloAccessTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        protected override void IncludeAuthorizationParameters(ref IDictionary<string, object> parameters)
        {
            if (!_tokenProvider.CanProvideTrelloAccessToken)
            {
                return;
            }

            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            if (!parameters.ContainsKey("key"))
            {
                parameters.Add("key", ApplicationKeys.TRELLO_APPLICATION_KEY);
            }

            if (!parameters.ContainsKey("token"))
            {
                parameters.Add("token", _tokenProvider.GetTrelloAccessToken());
            }
        }
    }
}