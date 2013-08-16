using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatters;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CardProcessor : ISingleBoardItemProcessor<Card>
    {
        private readonly UserContext _userContext;
        private readonly ISummaryFormatter<Card> _summaryFormatter;
        private readonly ITimeFrameFormatter<Card> _timeFrameFormatter;
        private readonly ILocationFormatter<Card> _locationFormatter;
        private readonly IDescriptionFormatter<Card> _descriptionFormatter;
        private readonly IExtendedPropertiesFormatter<Card> _extendedPropertiesFormatter;

        public CardProcessor(UserContext userContext, ISummaryFormatter<Card> summaryFormatter, ITimeFrameFormatter<Card> timeFrameFormatter, 
                             ILocationFormatter<Card> locationFormatter, IDescriptionFormatter<Card> descriptionFormatter,
                             IExtendedPropertiesFormatter<Card> extendedPropertiesFormatter)
        {
            _userContext = userContext;
            _summaryFormatter = summaryFormatter;
            _timeFrameFormatter = timeFrameFormatter;
            _locationFormatter = locationFormatter;
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

            return new Event
                {
                    Summary = _summaryFormatter.Format(item, _userContext.GetUserPreferences()),
                    Start = timeFrame.Item1,
                    End = timeFrame.Item2,
                    Location = _locationFormatter.Format(item, _userContext.GetUserPreferences()),
                    Description = _descriptionFormatter.Format(item, _userContext.GetUserPreferences()),
                    ExtendedProperties = _extendedPropertiesFormatter.Format(item)
                };
        }
    }
}
