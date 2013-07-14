using System.Collections.Generic;
using System.Linq;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class CardProcessor : ICardProcessor
    {
        public IEnumerable<Event> Process(Card card, string listName)
        {
            if (card.Due == null)
            {
                return Enumerable.Empty<Event>();
            }

            var @event = new Event
                {
                    summary = card.Name,
                    start = new TimeStamp(card.Due.Value),
                    end = new TimeStamp(card.Due.Value.AddHours(1))
                };

            return new[] { @event };
        }
    }
}