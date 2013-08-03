using System;
using Newtonsoft.Json;

namespace Trellendar.Domain.Calendar
{
    public class TimeStamp
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(Core.Serialization._Impl.JsonSerializer.DateConverter))]
        public DateTime? Date { get; set; }

        [JsonProperty("dateTime")]
        public DateTime? DateTime { get; set; }
    }
}
