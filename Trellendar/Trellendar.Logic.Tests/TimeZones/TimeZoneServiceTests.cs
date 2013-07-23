using System;
using NUnit.Framework;
using Trellendar.Logic.TimeZones._Impl;

namespace Trellendar.Logic.Tests.TimeZones
{
    public class TimeZoneServiceTests
    {
        [Test]
        public void returns_proper_UTC_offset_for_time_zone(
            [Values("Europe/Brussels")] string timeZone,
            [Values(2)] int expectedOffset)
        {
            // Arrange
            var offset = new TimeSpan(expectedOffset, 0, 0);

            // Act
            var result = new TimeZoneService().GetUtcOffset(timeZone);

            // Assert
            Assert.AreEqual(offset, result);
        }
    }
}
