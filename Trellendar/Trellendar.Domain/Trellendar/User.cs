using System;
using System.Collections.Generic;

namespace Trellendar.Domain.Trellendar
{
    public class User
    {
        #region TO REMOVE

        public string BoardID { get; set; }

        public string CalendarID { get; set; }

        public string CalendarTimeZone { get; set; }

        #endregion

        public Guid UserID { get; set; }

        public string Email { get; set; }

        public string GoogleAccessToken { get; set; }

        public DateTime GoogleAccessTokenExpirationTS { get; set; }

        public string GoogleRefreshToken { get; set; }

        public string TrelloAccessToken { get; set; }

        public DateTime LastSynchronizationTS { get; set; }

        public virtual IList<BoardCalendarBond> BoardCalendarBonds { get; set; }

        public virtual UserPreferences UserPreferences { get; set; }
    }
}
