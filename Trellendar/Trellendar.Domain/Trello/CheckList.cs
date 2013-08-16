using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trellendar.Domain.Trello
{
    public class CheckList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idCard")]
        public string IdCard { get; set; }

        [JsonIgnore]
        public Card Card { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("checkItems")]
        public IList<CheckItem> CheckItems { get; set; }
    }
}
