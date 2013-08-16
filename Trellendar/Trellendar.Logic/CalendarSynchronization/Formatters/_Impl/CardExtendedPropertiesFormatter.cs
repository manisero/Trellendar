using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CardExtendedPropertiesFormatter : ICardExtendedPropertiesFormatter
    {
        public EventExtendedProperties Format(Card card)
        {
            return EventExtensions.CreateExtendedProperties(card.Id);
        }
    }
}