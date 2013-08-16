using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CardProcessor : ISingleBoardItemProcessor<Card>
    {
        private readonly UserContext _userContext;
        private readonly IParser<BoardItemName> _boardItemNameParser;
        private readonly IParser<Due> _dueParser;
        private readonly IParser<Location> _locationParser;
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;

        public CardProcessor(UserContext userContext, IParser<BoardItemName> boardItemNameParser, IParser<Due> dueParser,
                             IParser<Location> locationParser, IEventTimeFrameCreator eventTimeFrameCreator)
        {
            _userContext = userContext;
            _boardItemNameParser = boardItemNameParser;
            _dueParser = dueParser;
            _locationParser = locationParser;
            _eventTimeFrameCreator = eventTimeFrameCreator;
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
                var due = _dueParser.Parse(item.Desc, _userContext.GetUserPreferences());

                if (due == null)
                {
                    return null;
                }

                timeFrame = due.HasTime
                                ? _eventTimeFrameCreator.CreateFromLocal(due.DueDateTime, _userContext.GetCalendarTimeZone(), _userContext.GetPrefferedWholeDayEventDueTime())
                                : _eventTimeFrameCreator.CreateWholeDayTimeFrame(due.DueDateTime);
            }

            var location = _locationParser.Parse(item.Desc, _userContext.GetUserPreferences());

            return new Event
                {
                    Summary = FormatEventSummary(item.Name, parentName),
                    Start = timeFrame.Item1,
                    End = timeFrame.Item2,
                    Location = location != null ? location.Value : null,
                    Description = item.Desc,
                    ExtendedProperties = EventExtensions.CreateExtendedProperties(item.Id)
                };
        }

        private string FormatEventSummary(string cardName, string listName)
        {
            var eventNameTemplate = _userContext.GetPrefferedCardEventNameTemplate();

            if (eventNameTemplate == null)
            {
                return cardName;
            }

            var parsedlistName = _boardItemNameParser.Parse(listName, _userContext.GetUserPreferences());

            return eventNameTemplate.FormatWith(parsedlistName != null ? parsedlistName.Value : null, cardName);
        }
    }
}
