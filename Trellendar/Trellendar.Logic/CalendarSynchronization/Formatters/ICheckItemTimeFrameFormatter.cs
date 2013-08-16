using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.Formatters
{
    public interface ICheckItemTimeFrameFormatter
    {
        Tuple<TimeStamp, TimeStamp> Format(CheckItem checkItem, User user);
    }
}
