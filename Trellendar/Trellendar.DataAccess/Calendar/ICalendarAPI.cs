using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Calendar
{
    public interface ICalendarAPI
    {
        string GetAuthorizationUri();

        Token GetToken(string authorizationCode);

        Token GetNewToken(string refreshToken);

        Domain.Calendar.Calendar GetCalendar(string calendarId);
    }
}
