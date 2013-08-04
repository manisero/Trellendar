using System;
using NUnit.Framework;
using Trellendar.Logic.CalendarSynchronization._Impl;

namespace Trellendar.Logic.Tests.CalendarSynchronization
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
            Assert.AreEqual(dateTime, result.Item1.Date);
            Assert.AreEqual(dateTime, result.Item2.Date);
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

            Assert.AreEqual(expectedStartDate, result.Item1.Date);
            Assert.AreEqual(expectedStartDateTime, result.Item1.DateTime);

            Assert.AreEqual(expectedEndDate, result.Item2.Date);
            Assert.AreEqual(expectedEndDateTime, result.Item2.DateTime);
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

            Assert.AreEqual(expectedStartDate, result.Item1.Date);
            Assert.AreEqual(expectedStartDateTime, result.Item1.DateTime);

            Assert.AreEqual(expectedEndDate, result.Item2.Date);
            Assert.AreEqual(expectedEndDateTime, result.Item2.DateTime);
        }
    }
}
