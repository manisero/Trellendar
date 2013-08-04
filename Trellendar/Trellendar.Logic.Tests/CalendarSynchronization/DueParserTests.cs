using System;
using NUnit.Framework;
using Trellendar.Logic.CalendarSynchronization._Impl;

namespace Trellendar.Logic.Tests.CalendarSynchronization
{
    [TestFixture]
    public class DueParserTests : TestsBase
    {
        [Test]
        public void parses_single_due_in_text(
            [Values("text [2013-07-05 10:12:30] text")] string textWithDue,
            [Values("2013-07-05 10:12:30")] string expectedDue)
        {
            // Arrange
            var due = DateTime.Parse(expectedDue);

            // Act
            var result = AutoMoqer.Resolve<DueParser>().Parse(textWithDue);

            // Assert
            Assert.AreEqual(due, result);
        }
    }
}
