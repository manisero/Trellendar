using Trellendar.Core.Extensions;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CheckItemProcessor : ISingleBoardItemProcessor<CheckItem>
    {
        private readonly UserContext _userContext;
        private readonly IParser<BoardItemName> _boardItemNameParser;
        private readonly IParser<Due> _dueParser;
        private readonly IParser<Location> _locationParser;
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;

        public CheckItemProcessor(UserContext userContext, IParser<BoardItemName> boardItemNameParser, IParser<Due> dueParser,
                                  IParser<Location> locationParser, IEventTimeFrameCreator eventTimeFrameCreator)
        {
            _userContext = userContext;
            _boardItemNameParser = boardItemNameParser;
            _dueParser = dueParser;
            _locationParser = locationParser;
            _eventTimeFrameCreator = eventTimeFrameCreator;
        }

        public string GetItemID(CheckItem item)
        {
            return item.Id;
        }

        public Event Process(CheckItem item, string parentName)
        {
            var due = _dueParser.Parse(item.Name, _userContext.GetUserPreferences());

            if (due == null)
            {
                return null;
            }

            var timeFrame = due.HasTime
                                ? _eventTimeFrameCreator.CreateFromLocal(due.DueDateTime, _userContext.GetCalendarTimeZone(), _userContext.GetPrefferedWholeDayEventDueTime())
                                : _eventTimeFrameCreator.CreateWholeDayTimeFrame(due.DueDateTime);

            var location = _locationParser.Parse(item.Name, _userContext.GetUserPreferences());

            return new Event
                {
                    Summary = FormatEventSummary(item, parentName),
                    Start = timeFrame.Item1,
                    End = timeFrame.Item2,
                    Location = location != null ? location.Value : null,
                    ExtendedProperties = EventExtensions.CreateExtendedProperties(item.Id)
                };
        }

        private string FormatEventSummary(CheckItem checkItem, string checkListName)
        {
            var eventNameTemplate = _userContext.GetPrefferedCheckListEventNameTemplate();

	        string summary;
            if (eventNameTemplate != null)
            {
                var parsedCheckListName = _boardItemNameParser.Parse(checkListName, _userContext.GetUserPreferences());
                summary = eventNameTemplate.FormatWith(parsedCheckListName != null ? parsedCheckListName.Value : null, checkItem.Name);
            }
            else
            {
                summary = checkItem.Name;
            }

            if (checkItem.IsDone())
            {
                summary += _userContext.GetPrefferedCheckListEventDoneSuffix();
            }

            return summary;
        }
    }
}
