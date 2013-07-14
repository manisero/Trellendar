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

        protected override void PreprocessRequest(string resource, IDictionary<string, object> parameters)
        {
            if (parameters == null || !AccessTokenProvider.CanProvideAccessToken)
            {
                return;
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