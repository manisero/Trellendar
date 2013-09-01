using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting
{
    public interface IExtendedPropertiesFormatter<TEntity>
    {
        EventExtendedProperties Format(TEntity entoty);
    }
}
