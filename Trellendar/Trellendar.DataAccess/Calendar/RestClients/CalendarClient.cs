using Trellendar.DataAccess._Impl;

namespace Trellendar.DataAccess.Calendar.RestClients
{
    public class CalendarClient : RestClient
    {
        public CalendarClient() : base("https://www.googleapis.com/calendar/v3/")
        {
        }
    }
}