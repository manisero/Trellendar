using System;
using Trellendar.Core.Serialization;
using System.Linq;

namespace Trellendar.Logic.TimeZones._Impl
{
    public class TimeZoneService : ITimeZoneService
    {
        private readonly IXmlSerializer _xmlSerializer;

        public TimeZoneService(IXmlSerializer xmlSerializer)
        {
            _xmlSerializer = xmlSerializer;
        }

        public TimeSpan? GetUtcOffset(DateTime localDateTime, string timeZone)
        {
            var data = _xmlSerializer.Deserialize<supplementalData>(TimeZonesResources.TimeZones);
            var zoneMaping = data.windowsZones.mapTimezones.mapZone.FirstOrDefault(x => x.type == timeZone);

            if (zoneMaping == null)
            {
                return null;
            }

            try
            {
                var zoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneMaping.other);

                return zoneInfo.GetUtcOffset(localDateTime);
            }
            catch (Exception)
            {   
                return null;
            }
        }

        public DateTime? GetUTCDateTime(DateTime localDateTime, string timeZone)
        {
            return localDateTime - GetUtcOffset(localDateTime, timeZone);
        }

        public DateTime? GetLocalDateTime(DateTime utcDateTime, string timeZone)
        {
            return utcDateTime + GetUtcOffset(utcDateTime, timeZone);
        }
    }
}