using System;

namespace Trellendar.Domain.Trellendar
{
    public class UserPreferences
    {
        public int UserID { get; set; }

        public virtual User User { get; set; }

        public string ListShortcutBeginningMarker { get; set; }

        public string ListShortcutEndMarker { get; set; }

        public string CardEventNameTemplate { get; set; }

        public string CheckListEventNameTemplate { get; set; }

        public string CheckListEventDoneSuffix { get; set; }

        public TimeSpan? WholeDayEventDueTime { get; set; }

        public string DueTextBeginningMarker { get; set; }

        public string DueTextEndMarker { get; set; }
    }
}
