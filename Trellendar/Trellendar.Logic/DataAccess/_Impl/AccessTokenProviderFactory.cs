using System;
using System.Collections.Generic;
using Trellendar.Core.DependencyResolution;
using Trellendar.DataAccess;
using Trellendar.Domain;
using Trellendar.Logic.DataAccess.AccessTokensProviders;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.DataAccess._Impl
{
    public class AccessTokenProviderFactory : IAccessTokenProviderFactory
    {
        private readonly IDictionary<DomainType, Func<IAccessTokenProvider>> _providers = new Dictionary<DomainType, Func<IAccessTokenProvider>>();

        public AccessTokenProviderFactory(IDependencyResolver dependencyResolver)
        {
            _providers[DomainType.Trello] = () => new TrelloAccessTokenProvider(dependencyResolver.Resolve<UserContext>());
            _providers[DomainType.Calendar] = () => new CalendarAccessTokenProvider(dependencyResolver.Resolve<UserContext>(), dependencyResolver.Resolve<ICalendarAccessTokenExpirationHandler>());
        }

        public IAccessTokenProvider Create(DomainType domainType)
        {
            if (!_providers.ContainsKey(domainType))
            {
                throw new NotSupportedException("No AccessTokenProvider found for DomainType '{0}'.".FormatWith(domainType));
            }

            return _providers[domainType]();
        }
    }
}
