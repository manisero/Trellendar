using System;
using NUnit.Framework;
using Trellendar.Logic.TimeZones;

namespace Trellendar.Logic.Tests.CalendarSynchronization
{
    public partial class EventTimeFrameCreatorTests
    {
        [Test]
        [Sequential]
        public void creates_proper_non_whole_day_time_frame___from_UTC(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string expectedStart,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);
            var timeZone = "time zone";
            var wholeDayIndicator = new TimeSpan(1000);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            AutoMoqer.GetMock<ITimeZoneService>().Setup(x => x.GetLocalDateTime(time, timeZone)).Returns(new DateTime(wholeDayIndicator.Ticks + 1000));

            // Act & Assert
            TestCreateFromUTC(time, timeZone, wholeDayIndicator, null, start, null, end);
        }

        [Test]
        [Sequential]
        public void creates_non_whole_day_time_frame_for_null_time_zone___from_UTC(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string expectedStart,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            // Act & Assert
            TestCreateFromUTC(time, null, null, null, start, null, end);
        }

        [Test]
        [Sequential]
        public void creates_non_whole_day_time_frame_for_null_whole_day_indicator___from_UTC(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string expectedStart,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            // Act & Assert
            TestCreateFromUTC(time, "time zone", null, null, start, null, end);
        }

        [Test]
        [Sequential]
        public void creates_non_whole_day_time_frame_for_null_time_in_zone___from_UTC(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string expectedStart,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);
            var timeZone = "time zone";

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            AutoMoqer.GetMock<ITimeZoneService>().Setup(x => x.GetLocalDateTime(time, timeZone)).Returns((DateTime?)null);

            // Act & Assert
            TestCreateFromUTC(time, timeZone, null, null, start, null, end);
        }

        [Test]
        [Sequential]
        public void creates_proper_whole_day_time_frame___from_UTC(
            [Values("2011-05-17 01:30:00", "2012-03-12 23:30:00", "2013-07-24 01:26:05")] string utcDateTime,
            [Values("2011-05-17 03:30:00", "2012-03-13 03:30:00", "2013-07-23 18:26:05")] string localDateTime,
            [Values("2011-05-17 00:00:00", "2012-03-13 00:00:00", "2013-07-23 00:00:00")] string expectedStart,
            [Values("2011-05-17 00:00:00", "2012-03-13 00:00:00", "2013-07-23 00:00:00")] string expectedEnd)
        {
            // Arrange
            var utcTime = DateTime.Parse(utcDateTime);
            var timeZone = "time zone";
            var localTime = DateTime.Parse(localDateTime);
            var wholeDayIndicator = new TimeSpan(localTime.TimeOfDay.Ticks);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            AutoMoqer.GetMock<ITimeZoneService>().Setup(x => x.GetLocalDateTime(utcTime, timeZone)).Returns(localTime);

            // Act & Assert
            TestCreateFromUTC(utcTime, timeZone, wholeDayIndicator, start, null, end, null);
        }
    }
}
