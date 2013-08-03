using Newtonsoft.Json;

namespace Trellendar.Domain.Calendar
{
    public class Calendar
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }
    }
}
