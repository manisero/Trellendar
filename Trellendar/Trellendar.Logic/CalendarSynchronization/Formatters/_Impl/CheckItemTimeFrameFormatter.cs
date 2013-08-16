using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CheckItemTimeFrameFormatter : ICheckItemTimeFrameFormatter
    {
        private readonly IParser<Due> _dueParser;
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;

        public CheckItemTimeFrameFormatter(IParser<Due> dueParser, IEventTimeFrameCreator eventTimeFrameCreator)
        {
            _dueParser = dueParser;
            _eventTimeFrameCreator = eventTimeFrameCreator;
        }

        public Tuple<TimeStamp, TimeStamp> Format(CheckItem checkItem, User user)
        {
            if (checkItem == null || user == null)
            {
                return null;
            }

            var due = _dueParser.Parse(checkItem.Name, user.UserPreferences);

            if (due == null)
            {
                return null;
            }

            return due.HasTime
                       ? _eventTimeFrameCreator.CreateFromLocal(due.DueDateTime, user.CalendarTimeZone,
                                                                user.UserPreferences != null ? user.UserPreferences.WholeDayEventDueTime : null)
                       : _eventTimeFrameCreator.CreateWholeDayTimeFrame(due.DueDateTime);
        }
    }
}