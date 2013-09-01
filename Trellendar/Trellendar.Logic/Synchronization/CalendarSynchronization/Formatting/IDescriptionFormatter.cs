using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting
{
    public interface IDescriptionFormatter<TEntity>
    {
        string Format(TEntity entity, UserPreferences userPreferences);
    }
}
