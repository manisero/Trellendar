using System;
using Trellendar.Domain.Calendar;
using Trellendar.Logic.TimeZones;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class EventTimeFrameCreator : IEventTimeFrameCreator
    {
        private const int DEFAULT_EVENT_LENGTH = 1;

        private readonly ITimeZoneService _timeZoneService;

        public EventTimeFrameCreator(ITimeZoneService timeZoneService)
        {
            _timeZoneService = timeZoneService;
        }

        public Tuple<TimeStamp, TimeStamp> Create(DateTime utcDateTime, string timeZone, DateTime wholeDayIndicator)
        {
            var timeInZone = timeZone != null
                                 ? _timeZoneService.GetDateTimeInZone(utcDateTime, timeZone)
                                 : null;

            if (timeInZone != null && timeInZone.Value.TimeOfDay == wholeDayIndicator.TimeOfDay)
            {
                return Tuple.Create(new TimeStamp { date = utcDateTime.Date }, new TimeStamp { date = utcDateTime.Date });
            }
            else
            {
                return Tuple.Create(new TimeStamp { dateTime = utcDateTime }, new TimeStamp { dateTime = utcDateTime.AddHours(DEFAULT_EVENT_LENGTH) });
            }
        }
    }
}