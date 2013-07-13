using System;

namespace Trellendar.Domain.Trellendar
{
    public class User
    {
        public int UserID { get; set; }

        public string Email { get; set; }

        public string TrelloBoardID { get; set; }

        public string TrelloAccessToken { get; set; }

        public DateTime? TrelloAccessTokenExpirationTS { get; set; }

        public string CalendarID { get; set; }

        public string CalendarAccessToken { get; set; }

        public DateTime CalendarAccessTokenExpirationTS { get; set; }

        public string CalendarRefreshToken { get; set; }
    }
}
