using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters
{
    public class CardTimeFrameFormatter : ITimeFrameFormatter<Card>
    {
        private readonly IParser<Due> _dueParser;
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;

        public CardTimeFrameFormatter(IParser<Due> dueParser, IEventTimeFrameCreator eventTimeFrameCreator)
        {
            _dueParser = dueParser;
            _eventTimeFrameCreator = eventTimeFrameCreator;
        }

        public Tuple<TimeStamp, TimeStamp> Format(Card entity, BoardCalendarBond boardCalendarBond)
        {
            if (entity == null || boardCalendarBond == null)
            {
                return null;
            }

            Tuple<TimeStamp, TimeStamp> timeFrame;

            if (entity.Due != null)
            {
                timeFrame = _eventTimeFrameCreator.CreateFromUTC(entity.Due.Value, boardCalendarBond.CalendarTimeZone,
                                                                 boardCalendarBond.Settings != null ? boardCalendarBond.Settings.WholeDayEventDueTime : null);
            }
            else
            {
                var due = _dueParser.Parse(entity.Description, boardCalendarBond.Settings);

                if (due == null)
                {
                    return null;
                }

                timeFrame = due.HasTime
                                ? _eventTimeFrameCreator.CreateFromLocal(due.DueDateTime, boardCalendarBond.CalendarTimeZone,
                                                                         boardCalendarBond.Settings != null ? boardCalendarBond.Settings.WholeDayEventDueTime : null)
                                : _eventTimeFrameCreator.CreateWholeDayTimeFrame(due.DueDateTime);
            }

            return timeFrame;
        }
    }
}