using Trellendar.Domain;

namespace Trellendar.DataAccess.Remote
{
    public interface IAccessTokenProviderFactory
    {
        IAccessTokenProvider Create(DomainType domainType);
    }
}
