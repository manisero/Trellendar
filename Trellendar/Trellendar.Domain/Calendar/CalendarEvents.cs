using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trellendar.Domain.Calendar
{
    public class CalendarEvents : Calendar
    {
        [JsonProperty("items")]
        public IList<Event> Items { get; set; }
    }
}
