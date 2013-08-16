using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CheckItemExtendedPropertiesFormatter : ICheckItemExtendedPropertiesFormatter
    {
        public EventExtendedProperties Format(CheckItem checkItem)
        {
            return EventExtensions.CreateExtendedProperties(checkItem.Id);
        }
    }
}