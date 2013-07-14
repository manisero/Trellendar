using System.Collections.Generic;

namespace Trellendar.Domain.Trello
{
    public class Board
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<List> Lists { get; set; }

        public IList<Card> Cards { get; set; }
    }
}
