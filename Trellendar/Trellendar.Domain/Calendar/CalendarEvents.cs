using System.Collections.Generic;

namespace Trellendar.Domain.Calendar
{
    public class CalendarEvents : Calendar
    {
        public IList<Event> Items { get; set; }
    }
}
