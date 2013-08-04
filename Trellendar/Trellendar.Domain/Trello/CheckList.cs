using System.Collections.Generic;

namespace Trellendar.Domain.Trello
{
    public class CheckList
    {
        public string Id { get; set; }

        public string IdCard { get; set; }

        public string Name { get; set; }

        public IList<CheckItem> CheckItems { get; set; }
    }
}
