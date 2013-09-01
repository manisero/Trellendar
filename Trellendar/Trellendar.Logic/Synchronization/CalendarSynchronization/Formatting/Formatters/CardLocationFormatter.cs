using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters
{
    public class CardLocationFormatter : ILocationFormatter<Card>
    {
        private readonly IParser<Location> _locationParser;

        public CardLocationFormatter(IParser<Location> locationParser)
        {
            _locationParser = locationParser;
        }

        public string Format(Card entity, BoardCalendarBondSettings boardCalendarBondSettings)
        {
            var location = _locationParser.Parse(entity.Description, boardCalendarBondSettings);

            return location != null ? location.Value : null;
        }
    }
}
