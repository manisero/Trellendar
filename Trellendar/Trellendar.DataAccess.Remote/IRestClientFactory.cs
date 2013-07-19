using Trellendar.Domain;

namespace Trellendar.DataAccess.Remote
{
    public interface IRestClientFactory
    {
        IRestClient CreateClient(DomainType domainType);

        IRestClient CreateAuthorizedClient(DomainType domainType);
    }
}
