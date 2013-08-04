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

        [Test]
        public void returns_null_UTC_offset_for_not_existing_time_zone()
        {
            // Arrange
            var timeZone = "not existing time zone";

            // Act
            var result = new TimeZoneService(new XmlSerializer()).GetUtcOffset(new DateTime(), timeZone);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void returns_proper_UTC_DateTime_for_time_zone(
            [Values("2013-07-23 18:26:05")] string localDateTime,
            [Values("Europe/Brussels")] string timeZone,
            [Values("2013-07-23 16:26:05")] string expectedUtcDateTime)
        {
            // Arrange
            var localTime = DateTime.Parse(localDateTime);
            var utcTime = DateTime.Parse(expectedUtcDateTime);

            // Act
            var result = new TimeZoneService(new XmlSerializer()).GetUTCDateTime(localTime, timeZone);

            // Assert
            Assert.AreEqual(utcTime, result);
        }

        [Test]
        public void returns_null_UTC_DateTime_for_not_existing_time_zone()
        {
            // Arrange
            var timeZone = "not existing time zone";

            // Act
            var result = new TimeZoneService(new XmlSerializer()).GetUTCDateTime(new DateTime(), timeZone);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void returns_proper_local_DateTime_for_time_zone(
            [Values("2013-07-23 18:26:05")] string utcDateTime,
            [Values("Europe/Brussels")] string timeZone,
            [Values("2013-07-23 20:26:05")] string expectedLocalDateTime)
        {
            // Arrange
            var utcTime = DateTime.Parse(utcDateTime);
            var localTime = DateTime.Parse(expectedLocalDateTime);

            // Act
            var result = new TimeZoneService(new XmlSerializer()).GetLocalDateTime(utcTime, timeZone);

            // Assert
            Assert.AreEqual(localTime, result);
        }

        [Test]
        public void returns_null_local_DateTime_for_not_existing_time_zone()
        {
            // Arrange
            var timeZone = "not existing time zone";

            // Act
            var result = new TimeZoneService(new XmlSerializer()).GetLocalDateTime(new DateTime(), timeZone);

            // Assert
            Assert.IsNull(result);
        }
    }
}
