using System;
using NUnit.Framework;
using Trellendar.Core.Serialization._Impl;
using Trellendar.Logic.TimeZones._Impl;

namespace Trellendar.Logic.Tests.TimeZones
{
    public class TimeZoneServiceTests
    {
        [Test]
        [Sequential]
        public void returns_proper_UTC_offset_for_time_zone(
            [Values("2013-07-23 18:26:05")] string dateTime,
            [Values("Europe/Brussels")] string timeZone,
            [Values(2)] int expectedOffset)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);
            var offset = new TimeSpan(expectedOffset, 0, 0);

            // Act
            var result = new TimeZoneService(new XmlSerializer()).GetUtcOffset(time, timeZone);

            // Assert
            Assert.AreEqual(offset, result);
        }
    }
}
