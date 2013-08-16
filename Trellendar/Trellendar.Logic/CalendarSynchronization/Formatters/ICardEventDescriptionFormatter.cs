using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ICardEventDescriptionFormatter
    {
        string Format(Card card);
    }
}
