using Trellendar.Domain;

namespace Trellendar.DataAccess
{
    public interface IAccessTokenProviderFactory
    {
        IAccessTokenProvider Create(DomainType domainType);
    }
}
