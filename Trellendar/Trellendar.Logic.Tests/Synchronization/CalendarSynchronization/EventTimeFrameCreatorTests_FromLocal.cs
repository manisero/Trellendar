using System;
using NUnit.Framework;
using Trellendar.Logic.TimeZones;

namespace Trellendar.Logic.Tests.Synchronization.CalendarSynchronization
{
    public partial class EventTimeFrameCreatorTests
    {
        [Test]
        [Sequential]
        public void creates_proper_non_whole_day_time_frame___from_local(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05", "2013-07-23 18:26:05")] string localDateTime,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05", "2013-07-23 17:26:05")] string utcDateTime,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05", "2013-07-23 17:26:05")] string expectedStart,
            [Values("2012-03-13 02:00:00", "2013-07-23 20:26:05", "2013-07-23 18:26:05")] string expectedEnd)
        {
            // Arrange
            var localTime = DateTime.Parse(localDateTime);
            var utcTime = DateTime.Parse(utcDateTime);
            var timeZone = "time zone";
            var wholeDayIndicator = new TimeSpan(1000);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            AutoMoqer.GetMock<ITimeZoneService>().Setup(x => x.GetUTCDateTime(localTime, timeZone)).Returns(utcTime);

            // Act & Assert
            TestCreateFromLocal(localTime, timeZone, wholeDayIndicator, null, start, null, end);
        }

        [Test]
        [Sequential]
        public void creates_non_whole_day_time_frame_for_null_time_zone___from_local(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string expectedStart,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            // Act & Assert
            TestCreateFromLocal(time, null, null, null, start, null, end);
        }

        [Test]
        [Sequential]
        public void creates_non_whole_day_time_frame_for_null_whole_day_indicator___from_local(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05", "2013-07-23 18:26:05")] string localDateTime,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05", "2013-07-23 17:26:05")] string utcDateTime,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05", "2013-07-23 17:26:05")] string expectedStart,
            [Values("2012-03-13 02:00:00", "2013-07-23 20:26:05", "2013-07-23 18:26:05")] string expectedEnd)
        {
            // Arrange
            var localTime = DateTime.Parse(localDateTime);
            var utcTime = DateTime.Parse(utcDateTime);
            var timeZone = "time zone";

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            AutoMoqer.GetMock<ITimeZoneService>().Setup(x => x.GetUTCDateTime(localTime, timeZone)).Returns(utcTime);

            // Act & Assert
            TestCreateFromLocal(localTime, timeZone, null, null, start, null, end);
        }

        [Test]
        [Sequential]
        public void creates_non_whole_day_time_frame_for_null_UTC_time___from_local(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string expectedStart,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);
            var timeZone = "time zone";

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            AutoMoqer.GetMock<ITimeZoneService>().Setup(x => x.GetUTCDateTime(time, timeZone)).Returns((DateTime?)null);

            // Act & Assert
            TestCreateFromLocal(time, timeZone, null, null, start, null, end);
        }

        [Test]
        [Sequential]
        public void creates_proper_whole_day_time_frame___from_local(
            [Values("2011-05-17 03:30:00", "2012-03-13 03:30:00", "2013-07-23 18:26:05")] string localDateTime,
            [Values("2011-05-17 00:00:00", "2012-03-13 00:00:00", "2013-07-23 00:00:00")] string expectedStart,
            [Values("2011-05-17 00:00:00", "2012-03-13 00:00:00", "2013-07-23 00:00:00")] string expectedEnd)
        {
            // Arrange
            var localTime = DateTime.Parse(localDateTime);
            var wholeDayIndicator = new TimeSpan(localTime.TimeOfDay.Ticks);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            // Act & Assert
            TestCreateFromLocal(localTime, "time zone", wholeDayIndicator, start, null, end, null);
        }
    }
}
