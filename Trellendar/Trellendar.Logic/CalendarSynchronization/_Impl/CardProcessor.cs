using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class CardProcessor : ICardProcessor
    {
        public Event Process(Card card, string listName)
        {
            if (card.Closed || card.Due == null)
            {
                return null;
            }

            var @event = new Event
                {
                    summary = card.Name,
                    start = new TimeStamp(card.Due.Value),
                    end = new TimeStamp(card.Due.Value.AddHours(1)),
                    extendedProperties = new EventExtendedProperties
                        {
                            @private = new Dictionary<string, string> { { EventConstants.SOURCE_ID_PROPERTY_KEY, card.Id } }
                        }
                };

            return @event;
        }
    }
}