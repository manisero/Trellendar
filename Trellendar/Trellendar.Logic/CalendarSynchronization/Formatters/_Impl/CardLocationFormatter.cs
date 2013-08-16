using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CardLocationFormatter : ILocationFormatter<Card>
    {
        private readonly IParser<Location> _locationParser;

        public CardLocationFormatter(IParser<Location> locationParser)
        {
            _locationParser = locationParser;
        }

        public string Format(Card entity, UserPreferences userPreferences)
        {
            var location = _locationParser.Parse(entity.Description, userPreferences);

            return location != null ? location.Value : null;
        }
    }
}
