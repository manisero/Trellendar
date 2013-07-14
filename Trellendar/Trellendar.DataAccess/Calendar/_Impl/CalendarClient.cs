using System.Net.Http.Headers;
using Trellendar.DataAccess._Core._Impl;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class CalendarClient : RestClient, ICalendarClient
    {
        public CalendarClient() : base("https://www.googleapis.com/calendar/v3/")
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApplicationKeys.GOOGLE_API_ACCESS_TOKEN);
        }
    }
}