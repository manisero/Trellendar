using System;
using System.Collections.Generic;
using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Calendar.RestClients;
using Trellendar.DataAccess.Trello;
using Trellendar.DataAccess.Trello.RestClients;
using Trellendar.DataAccess._Core;

namespace Trellendar.DataAccess._Impl
{
    public class RestClientFactory : IRestClientFactory
    {
        private readonly IDictionary<RestClientType, Func<IRestClient>> _clients = new Dictionary<RestClientType, Func<IRestClient>>();
        private readonly IDictionary<RestClientType, Func<IRestClient>> _authorizedClients = new Dictionary<RestClientType, Func<IRestClient>>();

        public RestClientFactory(ITrelloAccessTokenProvider trelloAccessTokenProvider, ICalendarAccessTokenProvider calendarAccessTokenProvider)
        {
            _clients[RestClientType.Trello] = () => new TrelloClient();
            _clients[RestClientType.Calendar] = () => new CalendarClient();

            _authorizedClients[RestClientType.Trello] = () => new AuthorizedTrelloClient(trelloAccessTokenProvider);
            _authorizedClients[RestClientType.Calendar] = () => new AuthorizedCalendarClient(calendarAccessTokenProvider);
        }

        public IRestClient CreateClient(RestClientType clientType)
        {
            return _clients[clientType]();
        }

        public IRestClient CreateAuthorizedClient(RestClientType clientType)
        {
            return _authorizedClients[clientType]();
        }
    }
}