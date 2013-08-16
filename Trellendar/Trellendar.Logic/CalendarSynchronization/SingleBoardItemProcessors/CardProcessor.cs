using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatters;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CardProcessor : ISingleBoardItemProcessor<Card>
    {
        private readonly UserContext _userContext;
        private readonly ICardSummaryFormatter _summaryFormatter;
        private readonly ICardTimeFrameFormatter _timeFrameFormatter;
        private readonly IParser<Location> _locationParser;
        private readonly ICardDescriptionFormatter _descriptionFormatter;
        private readonly ICardExtendedPropertiesFormatter _extendedPropertiesFormatter;

        public CardProcessor(UserContext userContext, ICardSummaryFormatter summaryFormatter, ICardTimeFrameFormatter timeFrameFormatter, 
                             IParser<Location> locationParser, ICardDescriptionFormatter descriptionFormatter,
                             ICardExtendedPropertiesFormatter extendedPropertiesFormatter)
        {
            _userContext = userContext;
            _summaryFormatter = summaryFormatter;
            _timeFrameFormatter = timeFrameFormatter;
            _locationParser = locationParser;
            _descriptionFormatter = descriptionFormatter;
            _extendedPropertiesFormatter = extendedPropertiesFormatter;
        }

        public string GetItemID(Card item)
        {
            return item.Id;
        }

        public Event Process(Card item)
        {
            if (item.Closed)
            {
                return null;
            }

            var timeFrame = _timeFrameFormatter.Format(item, _userContext.User);

            if (timeFrame == null)
            {
                return null;
            }

            var summary = _summaryFormatter.Format(item, _userContext.GetUserPreferences());
            var location = _locationParser.Parse(item.Description, _userContext.GetUserPreferences());
            var description = _descriptionFormatter.Format(item);

            return new Event
                {
                    Summary = summary,
                    Start = timeFrame.Item1,
                    End = timeFrame.Item2,
                    Location = location != null ? location.Value : null,
                    Description = description,
                    ExtendedProperties = _extendedPropertiesFormatter.Format(item)
                };
        }
    }
}
