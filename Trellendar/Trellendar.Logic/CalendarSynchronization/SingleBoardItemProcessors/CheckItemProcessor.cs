using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CheckItemProcessor : ISingleBoardItemProcessor<CheckItem>
    {
        private readonly UserContext _userContext;
        private readonly IDueParser _dueParser;
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;

        public CheckItemProcessor(UserContext userContext, IDueParser dueParser, IEventTimeFrameCreator eventTimeFrameCreator)
        {
            _userContext = userContext;
            _dueParser = dueParser;
            _eventTimeFrameCreator = eventTimeFrameCreator;
        }

        public string GetItemID(CheckItem item)
        {
            return item.Id;
        }

        public Event Process(CheckItem item, string parentName)
        {
            var due = _dueParser.Parse(item.Name);

            if (due == null)
            {
                return null;
            }

            var timeFrame = due.HasTime
                                ? _eventTimeFrameCreator.CreateFromLocal(due.DueDateTime, _userContext.GetCalendarTimeZone(), _userContext.GetPrefferedWholeDayEventDueTime())
                                : _eventTimeFrameCreator.CreateWholeDayTimeFrame(due.DueDateTime);

            return new Event
                {
                    Summary = FormatEventSummary(item),
                    Start = timeFrame.Item1,
                    End = timeFrame.Item2,
                    ExtendedProperties = EventExtensions.CreateExtendedProperties(item.Id)
                };
        }

        private string FormatEventSummary(CheckItem checkItem)
        {
            return checkItem.IsDone()
                       ? checkItem.Name + _userContext.GetPrefferedCheckListEventDoneSuffix()
                       : checkItem.Name;
        }
    }
}
