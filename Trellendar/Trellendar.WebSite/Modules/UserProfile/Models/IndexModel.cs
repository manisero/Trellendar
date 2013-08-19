using System.Collections.Generic;

namespace Trellendar.WebSite.Modules.UserProfile.Models
{
    public class IndexModel
    {
        public string Email { get; set; }

        public IDictionary<string, string> AvailableCalendars { get; set; }
    }
}