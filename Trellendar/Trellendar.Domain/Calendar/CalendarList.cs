using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trellendar.Domain.Calendar
{
    public class CalendarList
    {
        [JsonProperty("items")]
        public IList<Calendar> Items { get; set; }
    }
}
