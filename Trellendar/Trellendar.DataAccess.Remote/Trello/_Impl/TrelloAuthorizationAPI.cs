using System.Collections.Generic;
using Trellendar.Core;
using Trellendar.Domain;

namespace Trellendar.DataAccess.Remote.Trello._Impl
{
    public class TrelloAuthorizationAPI : ITrelloAuthorizationAPI
    {
        private readonly IRestClientFactory _restClientFactory;

        private IRestClient _trelloClient;
        private IRestClient TrelloClient
        {
            get { return _trelloClient ?? (_trelloClient = _restClientFactory.CreateClient(DomainType.Trello)); }
        }

        public TrelloAuthorizationAPI(IRestClientFactory restClientFactory)
        {
            _restClientFactory = restClientFactory;
        }

        public string GetAuthorizationUri()
        {
            return TrelloClient.FormatRequestUri("authorize",
                                                 new Dictionary<string, object>
                                                     {
                                                         { "key", ApplicationKeys.TRELLO_APPLICATION_KEY },
                                                         { "name", ApplicationConstants.APPLICATION_NAME },
                                                         { "expiration", "never" },
                                                         { "response_type", "token" }
                                                     },
                                                 true);
        }
    }
}