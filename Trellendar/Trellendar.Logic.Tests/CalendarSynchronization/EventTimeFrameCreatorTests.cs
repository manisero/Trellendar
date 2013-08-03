using System;
using NUnit.Framework;
using Trellendar.Logic.CalendarSynchronization._Impl;
using Trellendar.Logic.TimeZones;

namespace Trellendar.Logic.Tests.CalendarSynchronization
{
    [TestFixture]
    public class EventTimeFrameCreatorTests : TestsBase
    {
        [Test]
        [Sequential]
        public void creates_proper_non_whole_day_time_frame(
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

            AutoMoqer.GetMock<ITimeZoneService>().Setup(x => x.GetDateTimeInZone(time, timeZone)).Returns(new DateTime(wholeDayIndicator.Ticks + 1000));

            // Act
            var result = AutoMoqer.Resolve<EventTimeFrameCreator>().Create(time, timeZone, wholeDayIndicator);

            // Assert
            Assert.AreEqual(start, result.Item1.dateTime);
            Assert.IsNull(result.Item1.date);

            Assert.AreEqual(end, result.Item2.dateTime);
            Assert.IsNull(result.Item2.date);
        }

        [Test]
        [Sequential]
        public void creates_non_whole_day_time_frame_for_null_time_zone(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string expectedStart,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            // Act
            var result = AutoMoqer.Resolve<EventTimeFrameCreator>().Create(time, null, null);

            // Assert
            Assert.AreEqual(start, result.Item1.dateTime);
            Assert.IsNull(result.Item1.date);

            Assert.AreEqual(end, result.Item2.dateTime);
            Assert.IsNull(result.Item2.date);
        }

        [Test]
        [Sequential]
        public void creates_non_whole_day_time_frame_for_null_whole_day_indicator(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string expectedStart,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            // Act
            var result = AutoMoqer.Resolve<EventTimeFrameCreator>().Create(time, "time zone", null);

            // Assert
            Assert.AreEqual(start, result.Item1.dateTime);
            Assert.IsNull(result.Item1.date);

            Assert.AreEqual(end, result.Item2.dateTime);
            Assert.IsNull(result.Item2.date);
        }

        [Test]
        [Sequential]
        public void creates_non_whole_day_time_frame_for_null_time_in_zone(
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 18:26:05")] string expectedStart,
            [Values("2012-03-13 01:00:00", "2013-07-23 19:26:05")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);
            var timeZone = "time zone";

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            AutoMoqer.GetMock<ITimeZoneService>().Setup(x => x.GetDateTimeInZone(time, timeZone)).Returns((DateTime?)null);

            // Act
            var result = AutoMoqer.Resolve<EventTimeFrameCreator>().Create(time, timeZone, null);

            // Assert
            Assert.AreEqual(start, result.Item1.dateTime);
            Assert.IsNull(result.Item1.date);

            Assert.AreEqual(end, result.Item2.dateTime);
            Assert.IsNull(result.Item2.date);
        }

        [Test]
        [Sequential]
        public void creates_proper_whole_day_time_frame(
            [Values("2012-03-13 03:30:00", "2013-07-23 18:26:05")] string dateTime,
            [Values("2012-03-13 00:00:00", "2013-07-23 00:00:00")] string expectedStart,
            [Values("2012-03-13 00:00:00", "2013-07-23 00:00:00")] string expectedEnd)
        {
            // Arrange
            var time = DateTime.Parse(dateTime);
            var timeZone = "time zone";
            var wholeDayIndicator = new TimeSpan(1000);

            var start = DateTime.Parse(expectedStart);
            var end = DateTime.Parse(expectedEnd);

            AutoMoqer.GetMock<ITimeZoneService>().Setup(x => x.GetDateTimeInZone(time, timeZone)).Returns(new DateTime(wholeDayIndicator.Ticks));

            // Act
            var result = AutoMoqer.Resolve<EventTimeFrameCreator>().Create(time, timeZone, wholeDayIndicator);

            // Assert
            Assert.AreEqual(start, result.Item1.date);
            Assert.IsNull(result.Item1.dateTime);

            Assert.AreEqual(end, result.Item2.date);
            Assert.IsNull(result.Item2.dateTime);
        }
    }
}
