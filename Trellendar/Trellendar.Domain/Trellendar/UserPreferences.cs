using System;

namespace Trellendar.Domain.Trellendar
{
    public class UserPreferences
    {
        public Guid UserID { get; set; }

        public virtual User User { get; set; }

        public string TrelloItemShortcutBeginningMarker { get; set; }

        public string TrelloItemShortcutEndMarker { get; set; }

        public string CardEventNameTemplate { get; set; }

        public string CheckListEventNameTemplate { get; set; }

        public string CheckListEventDoneSuffix { get; set; }

        public TimeSpan? WholeDayEventDueTime { get; set; }

        public string DueTextBeginningMarker { get; set; }

        public string DueTextEndMarker { get; set; }

        public string LocationTextBeginningMarker { get; set; }

        public string LocationTextEndMarker { get; set; }
    }
}
