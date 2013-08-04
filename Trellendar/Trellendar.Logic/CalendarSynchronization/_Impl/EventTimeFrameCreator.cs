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

        public Tuple<TimeStamp, TimeStamp> CreateFromUTC(DateTime utcDateTime, string timeZone, TimeSpan? wholeDayIndicator)
        {
            if (timeZone != null && wholeDayIndicator != null)
            {
                var timeInZone = _timeZoneService.GetDateTimeInZone(utcDateTime, timeZone);

                if (timeInZone != null && timeInZone.Value - timeInZone.Value.Date == wholeDayIndicator.Value)
                {
                    return Tuple.Create(new TimeStamp { Date = timeInZone.Value.Date }, new TimeStamp { Date = timeInZone.Value.Date });
                }
            }
            
            return Tuple.Create(new TimeStamp { DateTime = utcDateTime }, new TimeStamp { DateTime = utcDateTime.AddHours(DEFAULT_EVENT_LENGTH) });
        }

        public Tuple<TimeStamp, TimeStamp> CreateFromLocal(DateTime localDateTime, string timeZone, TimeSpan? wholeDayIndicator)
        {
            throw new NotImplementedException();
        }
    }
}