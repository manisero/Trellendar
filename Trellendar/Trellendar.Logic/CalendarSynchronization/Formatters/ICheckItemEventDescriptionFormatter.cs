using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ICheckItemEventDescriptionFormatter
    {
        string Format(CheckItem checkItem);
    }
}
