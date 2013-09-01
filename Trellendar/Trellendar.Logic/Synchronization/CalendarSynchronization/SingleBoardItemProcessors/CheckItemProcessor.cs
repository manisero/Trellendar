using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CheckItemProcessor : ISingleBoardItemProcessor<CheckItem>
    {
        private readonly BoardCalendarContext _boardCalendarContext;
        private readonly ISummaryFormatter<CheckItem> _summaryFormatter;
        private readonly ITimeFrameFormatter<CheckItem> _timeFrameFormatter;
        private readonly ILocationFormatter<CheckItem> _locationFormatter;
        private readonly IDescriptionFormatter<CheckItem> _descriptionFormatter;
        private readonly IExtendedPropertiesFormatter<CheckItem> _extendedPropertiesFormatter;

        public CheckItemProcessor(BoardCalendarContext boardCalendarContext, ISummaryFormatter<CheckItem> summaryFormatter,
                                  ITimeFrameFormatter<CheckItem> timeFrameFormatter, ILocationFormatter<CheckItem> locationFormatter,
                                  IDescriptionFormatter<CheckItem> descriptionFormatter,
                                  IExtendedPropertiesFormatter<CheckItem> extendedPropertiesFormatter)
        {
            _boardCalendarContext = boardCalendarContext;
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

            var timeFrame = _timeFrameFormatter.Format(item, _boardCalendarContext.BoardCalendarBond);

            if (timeFrame == null)
            {
                return null;
            }

            return new Event
                {
                    Summary = _summaryFormatter.Format(item, _boardCalendarContext.GetSettings()),
                    Start = timeFrame.Item1,
                    End = timeFrame.Item2,
                    Location = _locationFormatter.Format(item, _boardCalendarContext.GetSettings()),
                    Description = _descriptionFormatter.Format(item, _boardCalendarContext.GetSettings()),
                    ExtendedProperties = _extendedPropertiesFormatter.Format(item)
                };
        }
    }
}
