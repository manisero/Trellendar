using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trellendar.Domain.Calendar
{
    public class Event
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("start")]
        public TimeStamp Start { get; set; }

        [JsonProperty("end")]
        public TimeStamp End { get; set; }
        
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("sequence")]
        public int Sequence { get; set; }

        [JsonProperty("extendedProperties")]
        public EventExtendedProperties ExtendedProperties { get; set; }
    }

    public class EventExtendedProperties
    {
        [JsonProperty("private")]
        public IDictionary<string, string> Private { get; set; }

        [JsonProperty("shared")]
        public IDictionary<string, string> Shared { get; set; }
    }
}
