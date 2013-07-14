using System.Collections.Generic;

namespace Trellendar.Domain.Trello
{
    public class Board
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<List> Lists { get; set; }

        public IEnumerable<Card> Cards { get; set; }
    }
}
