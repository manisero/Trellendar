using System.Collections.Generic;
using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Calendar
{
    public interface ICalendarAPI
    {
        Domain.Calendar.Calendar GetCalendar(string calendarId);

        IEnumerable<Event> GetEvents(string calendarId);

        void CreateEvent(string calendarId, Event @event);
    }
}
