using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface IDescriptionFormatter<TEntity>
    {
        string Format(TEntity entity, UserPreferences userPreferences);
    }
}
