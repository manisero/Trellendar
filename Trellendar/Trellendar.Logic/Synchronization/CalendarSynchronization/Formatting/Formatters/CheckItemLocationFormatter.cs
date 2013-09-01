using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters
{
    public class CheckItemLocationFormatter : ILocationFormatter<CheckItem>
    {
        private readonly IParser<Location> _locationParser;

        public CheckItemLocationFormatter(IParser<Location> locationParser)
        {
            _locationParser = locationParser;
        }

        public string Format(CheckItem entity, BoardCalendarBondSettings boardCalendarBondSettings)
        {
            var location = _locationParser.Parse(entity.Name, boardCalendarBondSettings);

            return location != null ? location.Value : null;
        }
    }
}
