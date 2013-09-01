using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting
{
    public interface ITimeFrameFormatter<TEntity>
    {
        Tuple<TimeStamp, TimeStamp> Format(TEntity entity, BoardCalendarBond boardCalendarBond);
    }
}
