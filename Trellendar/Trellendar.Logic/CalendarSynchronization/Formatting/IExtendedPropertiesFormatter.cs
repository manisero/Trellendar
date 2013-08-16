using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.CalendarSynchronization.Formatting
{
    public interface IExtendedPropertiesFormatter<TEntity>
    {
        EventExtendedProperties Format(TEntity entoty);
    }
}
