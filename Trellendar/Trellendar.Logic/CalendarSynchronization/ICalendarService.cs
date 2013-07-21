using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface ICalendarService
    {
        void UpdateCalendar(Board board, IEnumerable<Event> events);
    }
}
