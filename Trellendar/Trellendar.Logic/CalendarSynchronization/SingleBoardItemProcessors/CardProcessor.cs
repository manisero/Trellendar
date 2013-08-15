using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CardProcessor : SingleBoardItemProcessorBase, ISingleBoardItemProcessor<Card>
    {
        private readonly UserContext _userContext;
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;
        private readonly IParser<Due> _dueParser;

        public CardProcessor(UserContext userContext, IEventTimeFrameCreator eventTimeFrameCreator, IParser<Due> dueParser)
        {
            _userContext = userContext;
            _eventTimeFrameCreator = eventTimeFrameCreator;
            _dueParser = dueParser;
        }

        public string GetItemID(Card item)
        {
            return item.Id;
        }

        public Event Process(Card item, string parentName)
        {
            if (item.Closed)
            {
                return null;
            }

            Tuple<TimeStamp, TimeStamp> timeFrame;

            if (item.Due != null)
            {
                timeFrame = _eventTimeFrameCreator.CreateFromUTC(item.Due.Value, _userContext.GetCalendarTimeZone(), _userContext.GetPrefferedWholeDayEventDueTime());
            }
            else
            {
                var due = _dueParser.Parse(item.Desc);

                if (due == null)
                {
                    return null;
                }

                timeFrame = due.HasTime
                                ? _eventTimeFrameCreator.CreateFromLocal(due.DueDateTime, _userContext.GetCalendarTimeZone(), _userContext.GetPrefferedWholeDayEventDueTime())
                                : _eventTimeFrameCreator.CreateWholeDayTimeFrame(due.DueDateTime);
            }

            return new Event
                {
                    Summary = FormatEventSummary(item.Name, parentName),
                    Start = timeFrame.Item1,
                    End = timeFrame.Item2,
                    ExtendedProperties = EventExtensions.CreateExtendedProperties(item.Id)
                };
        }

        private string FormatEventSummary(string cardName, string listName)
        {
            var eventNameTemplate = _userContext.GetPrefferedCardEventNameTemplate();

            return eventNameTemplate != null
                       ? eventNameTemplate.FormatWith(FormatParentName(listName, _userContext.GetPrefferedListShortcutMarkers()), cardName)
                       : cardName;
        }
    }
}
