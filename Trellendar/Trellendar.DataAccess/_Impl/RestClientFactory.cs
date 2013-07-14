using System;
using System.Collections.Generic;
using Trellendar.Core.DependencyResolution;
using Trellendar.DataAccess.Calendar.RestClients;
using Trellendar.DataAccess.Trello.RestClients;
using Trellendar.Domain;

namespace Trellendar.DataAccess._Impl
{
    public class RestClientFactory : IRestClientFactory
    {
        private readonly IDictionary<DomainType, Func<IRestClient>> _clients = new Dictionary<DomainType, Func<IRestClient>>();
        private readonly IDictionary<DomainType, Func<IRestClient>> _authorizedClients = new Dictionary<DomainType, Func<IRestClient>>();

        public RestClientFactory(IDependencyResolver dependencyResolver)
        {
            _clients[DomainType.Trello] = () => new TrelloClient();
            _clients[DomainType.Calendar] = () => new CalendarClient();

            _authorizedClients[DomainType.Trello] = () => new AuthorizedTrelloClient(dependencyResolver.Resolve<IAccessTokenProviderFactory>());
            _authorizedClients[DomainType.Calendar] = () => new AuthorizedCalendarClient(dependencyResolver.Resolve<IAccessTokenProviderFactory>());
        }

        public IRestClient CreateClient(DomainType domainType)
        {
            return _clients[domainType]();
        }

        public IRestClient CreateAuthorizedClient(DomainType domainType)
        {
            return _authorizedClients[domainType]();
        }
    }
}