using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Calendar
{
    public interface ICalendarAPI
    {
        void Authorize();

        Token GetToken(string authorizationCode);
    }
}
