using System.Net.Http.Headers;
using Trellendar.Domain;

namespace Trellendar.DataAccess.Remote.Calendar.RestClients
{
    public class AuthorizedCalendarClient : CalendarClient
    {
        private readonly IAccessTokenProviderFactory _accessTokenProviderFactory;

        private IAccessTokenProvider _accessTokenProvider;
        private IAccessTokenProvider AccessTokenProvider
        {
            get { return _accessTokenProvider ?? (_accessTokenProvider = _accessTokenProviderFactory.Create(DomainType.Calendar)); }
        }

        public AuthorizedCalendarClient(IAccessTokenProviderFactory accessTokenProviderFactory)
        {
            _accessTokenProviderFactory = accessTokenProviderFactory;
        }

        protected override void PreprocessRequest(string resource, System.Collections.Generic.IDictionary<string, object> parameters)
        {
            if (AccessTokenProvider.CanProvideAccessToken)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessTokenProvider.GetAccessToken());
            }
        }
    }
}