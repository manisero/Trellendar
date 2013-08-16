using Newtonsoft.Json;

namespace Trellendar.Domain.Trello
{
    public class List
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
