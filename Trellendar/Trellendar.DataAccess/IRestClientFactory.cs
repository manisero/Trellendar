using Trellendar.DataAccess._Core;
using Trellendar.Domain;

namespace Trellendar.DataAccess
{
    public interface IRestClientFactory
    {
        IRestClient CreateClient(DomainType domainType);

        IRestClient CreateAuthorizedClient(DomainType domainType);
    }
}
