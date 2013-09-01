using System;
using NUnit.Framework;
using Trellendar.Logic.Synchronization.CalendarSynchronization._Impl;

namespace Trellendar.Logic.Tests.Synchronization.CalendarSynchronization
{
    [TestFixture]
    public partial class EventTimeFrameCreatorTests : TestsBase
    {
        [Test]
        public void creates_whole_day_time_frame()
        {
            // Arrange
            var dateTime = new DateTime(2012, 7, 3);

            // Act
            var result = AutoMoqer.Resolve<EventTimeFrameCreator>().CreateWholeDayTimeFrame(dateTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Item1);
            Assert.IsNotNull(result.Item2);

            AssertDateTime(dateTime, result.Item1.Date);
            AssertDateTime(null, result.Item1.DateTime);

            AssertDateTime(dateTime, result.Item2.Date);
            AssertDateTime(null, result.Item2.DateTime);
        }

        private void TestCreateFromUTC(DateTime utcTime, string timeZone, TimeSpan? wholeDayIndicator,
                                DateTime? expectedStartDate, DateTime? expectedStartDateTime,
                                DateTime? expectedEndDate, DateTime? expectedEndDateTime)
        {
            // Act
            var result = AutoMoqer.Resolve<EventTimeFrameCreator>().CreateFromUTC(utcTime, timeZone, wholeDayIndicator);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Item1);
            Assert.IsNotNull(result.Item2);

            AssertDateTime(expectedStartDate, result.Item1.Date);
            AssertDateTime(expectedStartDateTime, result.Item1.DateTime);

            AssertDateTime(expectedEndDate, result.Item2.Date);
            AssertDateTime(expectedEndDateTime, result.Item2.DateTime);
        }

        private void TestCreateFromLocal(DateTime utcTime, string timeZone, TimeSpan? wholeDayIndicator,
                                         DateTime? expectedStartDate, DateTime? expectedStartDateTime,
                                         DateTime? expectedEndDate, DateTime? expectedEndDateTime)
        {
            // Act
            var result = AutoMoqer.Resolve<EventTimeFrameCreator>().CreateFromLocal(utcTime, timeZone, wholeDayIndicator);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Item1);
            Assert.IsNotNull(result.Item2);

            AssertDateTime(expectedStartDate, result.Item1.Date);
            AssertDateTime(expectedStartDateTime, result.Item1.DateTime);

            AssertDateTime(expectedEndDate, result.Item2.Date);
            AssertDateTime(expectedEndDateTime, result.Item2.DateTime);
        }

        private static void AssertDateTime(DateTime? expected, DateTime? actual)
        {
            Assert.AreEqual(expected, actual);

            if (actual != null)
            {
                Assert.AreEqual(DateTimeKind.Utc, actual.Value.Kind);
            }
        }
    }
}
