using System;
using NUnit.Framework;
using Trellendar.Logic.CalendarSynchronization._Impl;

namespace Trellendar.Logic.Tests.CalendarSynchronization
{
    [TestFixture]
    public partial class EventTimeFrameCreatorTests : TestsBase
    {
        private void TestCreateFromUTC(DateTime utcTime, string timeZone, TimeSpan? wholeDayIndicator,
                                DateTime? expectedStartDate, DateTime? expectedStartDateTime,
                                DateTime? expectedEndDate, DateTime? expectedEndDateTime)
        {
            // Act
            var result = AutoMoqer.Resolve<EventTimeFrameCreator>().CreateFromUTC(utcTime, timeZone, wholeDayIndicator);

            // Assert
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
            Assert.AreEqual(expectedStartDate, result.Item1.Date);
            Assert.AreEqual(expectedStartDateTime, result.Item1.DateTime);

            Assert.AreEqual(expectedEndDate, result.Item2.Date);
            Assert.AreEqual(expectedEndDateTime, result.Item2.DateTime);
        }
    }
}
