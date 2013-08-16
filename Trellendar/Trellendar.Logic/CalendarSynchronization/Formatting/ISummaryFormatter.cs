using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.CalendarSynchronization.Formatting
{
    public interface ISummaryFormatter<TEntity>
    {
        string Format(TEntity entity, UserPreferences userPreferences);
    }
}
