using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ILocationFormatter<TEntity>
    {
        string Format(TEntity entity, UserPreferences userPreferences);
    }
}
