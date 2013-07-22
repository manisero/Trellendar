using System.Collections.Generic;
using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface IBoardItemsProcessor
    {
        void Process<TItem>(IEnumerable<TItem> items, string itemParentName, IEnumerable<Event> calendarEvents);
    }
}
