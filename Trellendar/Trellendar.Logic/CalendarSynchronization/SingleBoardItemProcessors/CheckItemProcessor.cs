using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatting;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CheckItemProcessor : ISingleBoardItemProcessor<CheckItem>
    {
        private readonly UserContext _userContext;
        private readonly ISummaryFormatter<CheckItem> _summaryFormatter;
        private readonly ITimeFrameFormatter<CheckItem> _timeFrameFormatter;
        private readonly ILocationFormatter<CheckItem> _locationFormatter;
        private readonly IDescriptionFormatter<CheckItem> _descriptionFormatter;
        private readonly IExtendedPropertiesFormatter<CheckItem> _extendedPropertiesFormatter;

        public CheckItemProcessor(UserContext userContext, ISummaryFormatter<CheckItem> summaryFormatter, ITimeFrameFormatter<CheckItem> timeFrameFormatter,
                                  ILocationFormatter<CheckItem> locationFormatter, IDescriptionFormatter<CheckItem> descriptionFormatter,
                                  IExtendedPropertiesFormatter<CheckItem> extendedPropertiesFormatter)
        {
            _userContext = userContext;
            _summaryFormatter = summaryFormatter;
            _timeFrameFormatter = timeFrameFormatter;
            _locationFormatter = locationFormatter;
            _descriptionFormatter = descriptionFormatter;
            _extendedPropertiesFormatter = extendedPropertiesFormatter;
        }

        public string GetItemID(CheckItem item)
        {
            return item.Id;
        }

        public Event Process(CheckItem item)
        {
            if (item.CheckList.Card.Closed)
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
