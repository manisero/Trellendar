using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ICardTimeFrameFormatter
    {
        Tuple<TimeStamp, TimeStamp> Format(Card card, User user);
    }
}
