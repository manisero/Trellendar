using System;

namespace Trellendar.Domain.Trellendar
{
    public class BoardCalendarBondSettings
    {
        public Guid UserID { get; set; }

        public string BoardID { get; set; }

        public string CalendarID { get; set; }

        public virtual BoardCalendarBond BoardCalendarBond { get; set; }

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

        public DateTime CreateTS { get; set; }

        public DateTime UpdateTS { get; set; }
    }
}
