using System;

namespace Trellendar.Domain.Trellendar
{
    public class BoardCalendarBond
    {
        public Guid UserID { get; set; }

        public virtual User User { get; set; }

        public string BoardID { get; set; }

        public string CalendarID { get; set; }

        public string CalendarTimeZone { get; set; }

        public DateTime CreateTS { get; set; }

        public virtual BoardCalendarBondSettings Settings { get; set; }
    }
}
