using System;

namespace Trellendar.Logic.CalendarSynchronization
{
    public class Due
    {
        public DateTime DueDateTime { get; set; }

        public bool HasTime { get; set; }
    }

    public interface IDueParser
    {
        Due Parse(string textWithDue);
    }
}
