using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters
{
    public class CheckItemExtendedPropertiesFormatter : IExtendedPropertiesFormatter<CheckItem>
    {
        public EventExtendedProperties Format(CheckItem entity)
        {
            return EventExtensions.CreateExtendedProperties(entity.Id);
        }
    }
}