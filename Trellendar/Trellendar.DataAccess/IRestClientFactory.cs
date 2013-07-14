using Trellendar.DataAccess._Core;

namespace Trellendar.DataAccess
{
    public interface IRestClientFactory
    {
        IRestClient CreateClient(RestClientType clientType);

        IRestClient CreateAuthorizedClient(RestClientType clientType);
    }
}
