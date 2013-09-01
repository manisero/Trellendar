using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters
{
    public class CheckItemTimeFrameFormatter : ITimeFrameFormatter<CheckItem>
    {
        private readonly IParser<Due> _dueParser;
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;

        public CheckItemTimeFrameFormatter(IParser<Due> dueParser, IEventTimeFrameCreator eventTimeFrameCreator)
        {
            _dueParser = dueParser;
            _eventTimeFrameCreator = eventTimeFrameCreator;
        }

        public Tuple<TimeStamp, TimeStamp> Format(CheckItem entity, BoardCalendarBond boardCalendarBond)
        {
            if (entity == null || boardCalendarBond == null)
            {
                return null;
            }

            var due = _dueParser.Parse(entity.Name, boardCalendarBond.Settings);

            if (due == null)
            {
                return null;
            }

            return due.HasTime
                       ? _eventTimeFrameCreator.CreateFromLocal(due.DueDateTime, boardCalendarBond.CalendarTimeZone,
                                                                boardCalendarBond.Settings != null ? boardCalendarBond.Settings.WholeDayEventDueTime : null)
                       : _eventTimeFrameCreator.CreateWholeDayTimeFrame(due.DueDateTime);
        }
    }
}