using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface ICardProcessor
    {
        Event Process(Card card, string listName);
    }
}
