using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ICardDescriptionFormatter
    {
        string Format(Card card);
    }
}
