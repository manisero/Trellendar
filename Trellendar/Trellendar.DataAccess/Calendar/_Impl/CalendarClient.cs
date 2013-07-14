using System.Net.Http.Headers;
using Trellendar.DataAccess._Core._Impl;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class CalendarClient : RestClient, ICalendarClient
    {
        public CalendarClient(ICalendarAccessTokenProvider tokenProvider) : base("https://www.googleapis.com/calendar/v3/")
        {
            if (tokenProvider.CanProvideCalendarAccessToken)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenProvider.GetCalendarAccessToken());
            }
        }
    }
}