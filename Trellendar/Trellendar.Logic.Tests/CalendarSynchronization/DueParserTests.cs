using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.CalendarSynchronization._Impl;

namespace Trellendar.Logic.Tests.CalendarSynchronization
{
    [TestFixture]
    public class DueParserTests : TestsBase
    {
        [Test]
        public void parses_single_due_in_text(
            [Values("[")] string beginningMarker,
            [Values("]")] string endMarker,
            [Values("text [2013-07-05 10:12:30] text")] string textWithDue,
            [Values("2013-07-05 10:12:30")] string expectedDue)
        {
            // Arrange
            var preferences = Builder<UserPreferences>.CreateNew()
                                                      .With(x => x.DueTextBeginningMarker = beginningMarker)
                                                      .With(x => x.DueTextEndMarker = endMarker)
                                                      .Build();

            var due = DateTime.Parse(expectedDue);

            MockUserContext(preferences);

            // Act
            var result = AutoMoqer.Resolve<DueParser>().Parse(textWithDue);

            // Assert
            Assert.AreEqual(due, result);
        }
    }
}
