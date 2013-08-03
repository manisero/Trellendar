using System;
using Newtonsoft.Json;

namespace Trellendar.Domain.Calendar
{
    public class TimeStamp
    {
        [JsonConverter(typeof(Core.Serialization._Impl.JsonSerializer.DateConverter))]
        public DateTime? date { get; set; }

        public DateTime? dateTime { get; set; }
    }
}
