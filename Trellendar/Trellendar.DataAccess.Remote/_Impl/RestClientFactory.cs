using System;
using System.Collections.Generic;
using Trellendar.Core.DependencyResolution;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Google;
using Trellendar.DataAccess.Remote.Trello.RestClients;
using Trellendar.Domain;

namespace Trellendar.DataAccess.Remote._Impl
{
    public class RestClientFactory : IRestClientFactory
    {
        private readonly IDictionary<DomainType, Func<IRestClient>> _clients = new Dictionary<DomainType, Func<IRestClient>>();
        private readonly IDictionary<DomainType, Func<IRestClient>> _authorizedClients = new Dictionary<DomainType, Func<IRestClient>>();

        public RestClientFactory(IDependencyResolver dependencyResolver)
        {
            _clients[DomainType.Trello] = () => new TrelloClient();
            _clients[DomainType.Calendar] = () => new GoogleClient();

            _authorizedClients[DomainType.Trello] = () => new AuthorizedTrelloClient(dependencyResolver.Resolve<IAccessTokenProviderFactory>());
            _authorizedClients[DomainType.Calendar] = () => new CalendarClient(dependencyResolver.Resolve<IAccessTokenProviderFactory>());
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