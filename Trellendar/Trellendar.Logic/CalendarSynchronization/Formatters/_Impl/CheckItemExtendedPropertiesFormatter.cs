using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CheckItemExtendedPropertiesFormatter : IExtendedPropertiesFormatter<CheckItem>
    {
        public EventExtendedProperties Format(CheckItem entity)
        {
            return EventExtensions.CreateExtendedProperties(entity.Id);
        }
    }
}