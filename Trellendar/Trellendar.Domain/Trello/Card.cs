using System;
using Newtonsoft.Json;

namespace Trellendar.Domain.Trello
{
    public class Card
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonIgnore]
        public List List { get; set; }

        [JsonProperty("idList")]
        public string IdList { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("due")]
        public DateTime? Due { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("dateLastActivity")]
        public DateTime DateLastActivity { get; set; }

        [JsonProperty("shortUrl")]
        public string Url { get; set; }
    }
}
