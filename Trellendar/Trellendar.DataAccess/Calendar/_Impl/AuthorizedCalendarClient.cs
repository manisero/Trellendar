using System.Net.Http.Headers;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class AuthorizedCalendarClient : CalendarClient, IAuthorizedCalendarClient
    {
        public AuthorizedCalendarClient(ICalendarAccessTokenProvider tokenProvider)
        {
            if (tokenProvider.CanProvideCalendarAccessToken)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenProvider.GetCalendarAccessToken());
            }
        }
    }
}