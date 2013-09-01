using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting
{
    public interface ISummaryFormatter<TEntity>
    {
        string Format(TEntity entity, BoardCalendarBondSettings boardCalendarBondSettings);
    }
}
