namespace Trellendar.DataAccess.Calendar.RestClients
{
    public class CalendarClient : RestClientBase
    {
        public CalendarClient() : base("https://www.googleapis.com/calendar/v3/")
        {
        }
    }
}