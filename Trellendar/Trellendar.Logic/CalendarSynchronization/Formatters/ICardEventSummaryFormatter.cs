using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ICardEventSummaryFormatter
    {
        string Format(Card card, UserPreferences userPreferences);
    }
}
