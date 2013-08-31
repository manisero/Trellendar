using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trellendar.Domain.Trello
{
    public class Board
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lists")]
        public IList<List> Lists { get; set; }

        [JsonProperty("cards")]
        public IList<Card> Cards { get; set; }

        [JsonProperty("checklists")]
        public IList<CheckList> CheckLists { get; set; }

        [JsonProperty("isClosed")]
        public bool IsClosed { get; set; }
    }
}
