using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ITimeFrameFormatter<TEntity>
    {
        Tuple<TimeStamp, TimeStamp> Format(TEntity entity, User user);
    }
}
