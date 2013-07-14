using System.Collections.Generic;
using Trellendar.Domain;

namespace Trellendar.DataAccess.Trello.RestClients
{
    public class AuthorizedTrelloClient : TrelloClient
    {
        private readonly IAccessTokenProviderFactory _accessTokenProviderFactory;

        private IAccessTokenProvider _accessTokenProvider;
        private IAccessTokenProvider AccessTokenProvider
        {
            get { return _accessTokenProvider ?? (_accessTokenProvider = _accessTokenProviderFactory.Create(DomainType.Trello)); }
        }

        public AuthorizedTrelloClient(IAccessTokenProviderFactory accessTokenProviderFactory)
        {
            _accessTokenProviderFactory = accessTokenProviderFactory;
        }

        protected override void IncludeAuthorizationParameters(ref IDictionary<string, object> parameters)
        {
            if (!AccessTokenProvider.CanProvideAccessToken)
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
                parameters.Add("token", AccessTokenProvider.GetAccessToken());
            }
        }
    }
}