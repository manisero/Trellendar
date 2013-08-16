using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CardExtendedPropertiesFormatter : IExtendedPropertiesFormatter<Card>
    {
        public EventExtendedProperties Format(Card entity)
        {
            return EventExtensions.CreateExtendedProperties(entity.Id);
        }
    }
}