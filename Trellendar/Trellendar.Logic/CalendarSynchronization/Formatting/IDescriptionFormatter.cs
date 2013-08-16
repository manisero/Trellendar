using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.CalendarSynchronization.Formatting
{
    public interface IDescriptionFormatter<TEntity>
    {
        string Format(TEntity entity, UserPreferences userPreferences);
    }
}
