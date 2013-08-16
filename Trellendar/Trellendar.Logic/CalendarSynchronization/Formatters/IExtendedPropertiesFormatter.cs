using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface IExtendedPropertiesFormatter<TEntity>
    {
        EventExtendedProperties Format(TEntity entoty);
    }
}
