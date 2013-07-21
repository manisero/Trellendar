using System.Collections.Generic;
using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface IBoardItemsProcessor<TItem>
    {
        void Process(IEnumerable<TItem> items, string itemParentName, IEnumerable<Event> calendarEvents);
    }
}
