using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.CalendarSynchronization.Formatting
{
    public interface ILocationFormatter<TEntity>
    {
        string Format(TEntity entity, UserPreferences userPreferences);
    }
}
