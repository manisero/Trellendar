using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ICheckItemDescriptionFormatter
    {
        string Format(CheckItem checkItem, UserPreferences userPreferences);
    }
}
