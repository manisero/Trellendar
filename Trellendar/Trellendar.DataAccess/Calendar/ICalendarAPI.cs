using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Calendar
{
    public interface ICalendarAPI
    {
        string GetAuthorizationUri();

        Token GetToken(string authorizationCode);
    }
}
