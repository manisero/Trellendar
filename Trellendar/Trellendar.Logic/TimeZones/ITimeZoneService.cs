using System;

namespace Trellendar.Logic.TimeZones
{
    public interface ITimeZoneService
    {
        TimeSpan? GetUtcOffset(DateTime dateTime, string timeZone);

        DateTime? GetDateTimeInZone(DateTime dateTime, string timeZone);
    }
}
