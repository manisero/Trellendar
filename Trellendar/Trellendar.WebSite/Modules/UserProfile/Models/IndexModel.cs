using System.Collections.Generic;

namespace Trellendar.WebSite.Modules.UserProfile.Models
{
    public class IndexModel
    {
        public string Email { get; set; }

        public IEnumerable<BoardCalendarBondModel> BoardCalendarBonds { get; set; }
    }
}