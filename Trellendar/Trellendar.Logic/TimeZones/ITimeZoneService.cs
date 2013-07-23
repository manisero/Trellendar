using System;

namespace Trellendar.Logic.TimeZones
{
    public interface ITimeZoneService
    {
        TimeSpan GetUtcOffset(string timeZone);
    }
}
