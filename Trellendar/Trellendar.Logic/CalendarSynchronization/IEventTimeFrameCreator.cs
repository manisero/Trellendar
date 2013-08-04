using System;
using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface IEventTimeFrameCreator
    {
        Tuple<TimeStamp, TimeStamp> CreateFromUTC(DateTime utcDateTime, string timeZone, TimeSpan? wholeDayIndicator);

        Tuple<TimeStamp, TimeStamp> CreateFromLocal(DateTime localDateTime, string timeZone, TimeSpan? wholeDayIndicator);
    }
}
