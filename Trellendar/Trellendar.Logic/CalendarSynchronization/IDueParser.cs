using System;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface IDueParser
    {
        DateTime? Parse(string textWithDue);
    }
}
