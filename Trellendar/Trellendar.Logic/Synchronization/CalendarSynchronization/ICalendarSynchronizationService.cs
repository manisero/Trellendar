using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization
{
    public interface ICalendarSynchronizationService
    {
        void UpdateCalendar(Board board, IEnumerable<Event> events);
    }
}
