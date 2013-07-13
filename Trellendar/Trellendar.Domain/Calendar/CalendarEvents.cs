using System.Collections.Generic;

namespace Trellendar.Domain.Calendar
{
    public class CalendarEvents : Calendar
    {
        public IEnumerable<Event> Items { get; set; }
    }
}
