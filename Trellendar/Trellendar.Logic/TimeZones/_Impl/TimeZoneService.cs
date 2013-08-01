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

        public TimeSpan? GetUtcOffset(DateTime dateTime, string timeZone)
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

                return zoneInfo.GetUtcOffset(dateTime);
            }
            catch (Exception)
            {   
                return null;
            }
        }

        public DateTime? GetDateTimeInZone(DateTime dateTime, string timeZone)
        {
            return dateTime + GetUtcOffset(dateTime, timeZone);
        }
    }
}