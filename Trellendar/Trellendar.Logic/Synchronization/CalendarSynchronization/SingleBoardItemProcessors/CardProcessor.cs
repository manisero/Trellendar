﻿using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CardProcessor : ISingleBoardItemProcessor<Card>
    {
        private readonly BoardCalendarContext _boardCalendarContext;
        private readonly ISummaryFormatter<Card> _summaryFormatter;
        private readonly ITimeFrameFormatter<Card> _timeFrameFormatter;
        private readonly ILocationFormatter<Card> _locationFormatter;
        private readonly IDescriptionFormatter<Card> _descriptionFormatter;
        private readonly IExtendedPropertiesFormatter<Card> _extendedPropertiesFormatter;

        public CardProcessor(BoardCalendarContext boardCalendarContext, ISummaryFormatter<Card> summaryFormatter,
                             ITimeFrameFormatter<Card> timeFrameFormatter, ILocationFormatter<Card> locationFormatter,
                             IDescriptionFormatter<Card> descriptionFormatter, IExtendedPropertiesFormatter<Card> extendedPropertiesFormatter)
        {
            _boardCalendarContext = boardCalendarContext;
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
