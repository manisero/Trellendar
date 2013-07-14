using System.Collections.Generic;
using Trellendar.Core;

namespace Trellendar.DataAccess.Trello._Impl
{
    public class TrelloAuthorizationAPI : ITrelloAuthorizationAPI
    {
        private readonly ITrelloClient _trelloClient;

        public TrelloAuthorizationAPI(ITrelloClient trelloClient)
        {
            _trelloClient = trelloClient;
        }

        public string GetAuthorizationUri()
        {
            return _trelloClient.FormatRequestUri("authorize",
                                                  new Dictionary<string, object>
                                                      {
                                                          { "key", ApplicationKeys.TRELLO_APPLICATION_KEY},
                                                          { "name", ApplicationConstants.APPLICATION_NAME },
                                                          { "expiration", "never" },
                                                          { "response_type", "token" }
                                                      },
                                                  true);
        }
    }
}