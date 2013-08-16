using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ISummaryFormatter<TEntity>
    {
        string Format(TEntity entity, UserPreferences userPreferences);
    }
}
