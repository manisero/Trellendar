using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatters;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CheckItemProcessor : ISingleBoardItemProcessor<CheckItem>
    {
        private readonly UserContext _userContext;
        private readonly ICheckItemSummaryFormatter _summaryFormatter;
        private readonly ICheckItemTimeFrameFormatter _timeFrameFormatter;
        private readonly ILocationFormatter<CheckItem> _locationFormatter;
        private readonly ICheckItemDescriptionFormatter _descriptionFormatter;
        private readonly ICheckItemExtendedPropertiesFormatter _extendedPropertiesFormatter;

        public CheckItemProcessor(UserContext userContext, ICheckItemSummaryFormatter summaryFormatter, ICheckItemTimeFrameFormatter timeFrameFormatter,
                                  ILocationFormatter<CheckItem> locationFormatter, ICheckItemDescriptionFormatter descriptionFormatter,
                                  ICheckItemExtendedPropertiesFormatter extendedPropertiesFormatter)
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
