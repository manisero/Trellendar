using System;
using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface IEventTimeFrameCreator
    {
        Tuple<TimeStamp, TimeStamp> Create(DateTime utcDateTime, string timeZone, DateTime wholeDayIndicator);
    }
}
