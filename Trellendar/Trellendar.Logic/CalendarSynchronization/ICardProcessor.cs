using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface ICardProcessor
    {
        IEnumerable<Event> Process(Card card, string listName);
    }
}
