using System;

namespace Trellendar.Domain.Trellendar
{
    public class User
    {
        public Guid UserID { get; set; }

        public string Email { get; set; }

        public string TrelloBoardID { get; set; }

        public string TrelloAccessToken { get; set; }

        public string CalendarID { get; set; }

        public string CalendarTimeZone { get; set; }

        public string CalendarAccessToken { get; set; }

        public DateTime CalendarAccessTokenExpirationTS { get; set; }

        public string CalendarRefreshToken { get; set; }

        public DateTime LastSynchronizationTS { get; set; }

        public virtual UserPreferences UserPreferences { get; set; }
    }
}
