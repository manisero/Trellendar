using System.Net.Http.Headers;
using Trellendar.Domain;

namespace Trellendar.DataAccess.Calendar.RestClients
{
    public class AuthorizedCalendarClient : CalendarClient
    {
        public AuthorizedCalendarClient(IAccessTokenProviderFactory accessTokenProviderFactory)
        {
            var provider = accessTokenProviderFactory.Create(DomainType.Calendar);

            if (provider.CanProvideAccessToken)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", provider.GetAccessToken());
            }
        }
    }
}