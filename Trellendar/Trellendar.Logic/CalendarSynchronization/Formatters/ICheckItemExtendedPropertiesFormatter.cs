using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ICheckItemExtendedPropertiesFormatter
    {
        EventExtendedProperties Format(CheckItem checkItem);
    }
}
