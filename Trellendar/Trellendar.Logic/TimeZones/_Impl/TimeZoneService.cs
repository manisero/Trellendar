using System;
using Trellendar.Core.Serialization;

namespace Trellendar.Logic.TimeZones._Impl
{
    public class TimeZoneService : ITimeZoneService
    {
        private readonly IXmlSerializer _xmlSerializer;

        public TimeZoneService(IXmlSerializer xmlSerializer)
        {
            _xmlSerializer = xmlSerializer;
        }

        public TimeSpan GetUtcOffset(string timeZone)
        {
            var data = _xmlSerializer.Deserialize<supplementalData>(TimeZonesResources.TimeZones);

            throw new NotImplementedException();
        }
    }
}