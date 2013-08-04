using System;

namespace Trellendar.Logic.TimeZones
{
    public interface ITimeZoneService
    {
        TimeSpan? GetUtcOffset(DateTime localDateTime, string timeZone);

        DateTime? GetUTCDateTime(DateTime localDateTime, string timeZone);

        DateTime? GetLocalDateTime(DateTime utcDateTime, string timeZone);
    }
}
