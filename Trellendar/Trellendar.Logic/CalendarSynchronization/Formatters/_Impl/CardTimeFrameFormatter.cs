using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CardTimeFrameFormatter : ICardTimeFrameFormatter
    {
        private readonly IParser<Due> _dueParser;
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;

        public CardTimeFrameFormatter(IParser<Due> dueParser, IEventTimeFrameCreator eventTimeFrameCreator)
        {
            _dueParser = dueParser;
            _eventTimeFrameCreator = eventTimeFrameCreator;
        }

        public Tuple<TimeStamp, TimeStamp> Format(Card card, User user)
        {
            if (card == null || user == null)
            {
                return null;
            }

            Tuple<TimeStamp, TimeStamp> timeFrame;

            if (card.Due != null)
            {
                timeFrame = _eventTimeFrameCreator.CreateFromUTC(card.Due.Value, user.CalendarTimeZone,
                                                                 user.UserPreferences != null ? user.UserPreferences.WholeDayEventDueTime : null);
            }
            else
            {
                var due = _dueParser.Parse(card.Description, user.UserPreferences);

                if (due == null)
                {
                    return null;
                }

                timeFrame = due.HasTime
                                ? _eventTimeFrameCreator.CreateFromLocal(due.DueDateTime, user.CalendarTimeZone,
                                                                         user.UserPreferences != null ? user.UserPreferences.WholeDayEventDueTime : null)
                                : _eventTimeFrameCreator.CreateWholeDayTimeFrame(due.DueDateTime);
            }

            return timeFrame;
        }
    }
}