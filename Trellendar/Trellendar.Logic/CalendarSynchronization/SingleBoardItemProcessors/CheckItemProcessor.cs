using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatters;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CheckItemProcessor : ISingleBoardItemProcessor<CheckItem>
    {
        private readonly UserContext _userContext;
        private readonly ICheckItemSummaryFormatter _summaryFormatter;
        private readonly ICheckItemTimeFrameFormatter _timeFrameFormatter;
        private readonly IParser<Location> _locationParser;
        private readonly ICheckItemDescriptionFormatter _descriptionFormatter;
        private readonly ICheckItemExtendedPropertiesFormatter _extendedPropertiesFormatter;

        public CheckItemProcessor(UserContext userContext, ICheckItemSummaryFormatter summaryFormatter, ICheckItemTimeFrameFormatter timeFrameFormatter, IParser<Location> locationParser,
                                  ICheckItemDescriptionFormatter descriptionFormatter, ICheckItemExtendedPropertiesFormatter extendedPropertiesFormatter)
        {
            _userContext = userContext;
            _summaryFormatter = summaryFormatter;
            _timeFrameFormatter = timeFrameFormatter;
            _locationParser = locationParser;
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

            var summary = _summaryFormatter.Format(item, _userContext.GetUserPreferences());
            var location = _locationParser.Parse(item.Name, _userContext.GetUserPreferences());
            var description = _descriptionFormatter.Format(item, _userContext.GetUserPreferences());

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
