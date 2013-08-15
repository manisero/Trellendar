using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.CalendarSynchronization.Parsers;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Parsers
{
    [TestFixture]
    public class DueParserTests : TestsBase
    {
        [Test]
        [Sequential]
        public void parses_single_due_with_time(
            [Values("[", "<due>", "[due]")] string beginningMarker,
            [Values("]", "</due>", "[endofdue]")] string endMarker,
            [Values("text [2013-07-05 10:12:30] text", 
                    "text <due>2012-11-25 03:27:00</due> text",
                    "text [due]2013-07-05 10:12:30[endofdue] text")] 
                    string textWithDue,
            [Values("2013-07-05 10:12:30", "2012-11-25 03:27:00", "2013-07-05 10:12:30")] string expectedDue)
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
            Assert.IsNotNull(result);
            Assert.AreEqual(due, result.DueDateTime);
            Assert.AreEqual(true, result.HasTime);
        }

        [Test]
        [Sequential]
        public void parses_single_due_without_time(
            [Values("[", "<due>", "[due]")] string beginningMarker,
            [Values("]", "</due>", "[endofdue]")] string endMarker,
            [Values("text [2013-07-05] text",
                    "text <due>2012-11-25</due> text",
                    "text [due]2013-07-05[endofdue] text")] 
                    string textWithDue,
            [Values("2013-07-05", "2012-11-25", "2013-07-05")] string expectedDue)
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
            Assert.IsNotNull(result);
            Assert.AreEqual(due, result.DueDateTime);
            Assert.AreEqual(false, result.HasTime);
        }

        [Test]
        [Sequential]
        public void handles_multiple_markers_in_text(
            [Values("[", "<due>", "[due]")] string beginningMarker,
            [Values("]", "</due>", "[endofdue]")] string endMarker,
            [Values("text [2013-07-05 10:12:30] text [not a due] text",
                    "text <due>not a due</due> text <due>2012-11-25 03:27:00</due> text",
                    "text [due]2013-07-05 10:12:30[endofdue] text [due]2012-11-25 03:27:00[endofdue] text")]
                    string textWithDue,
            [Values("2013-07-05 10:12:30", "2012-11-25 03:27:00", "2013-07-05 10:12:30")] string expectedDue)
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
            Assert.IsNotNull(result);
            Assert.AreEqual(due, result.DueDateTime);
            Assert.AreEqual(true, result.HasTime);
        }

        [Test]
        public void returns_null_for_unreadable_due(
            [Values(null, "[", "<")] string beginningMarker,
            [Values(null, "]", ">")] string endMarker,
            [Values("text [not a due] text", "no due")] string textWithDue)
        {
            // Arrange
            var preferences = Builder<UserPreferences>.CreateNew()
                                                      .With(x => x.DueTextBeginningMarker = beginningMarker)
                                                      .With(x => x.DueTextEndMarker = endMarker)
                                                      .Build();

            MockUserContext(preferences);

            // Act
            var result = AutoMoqer.Resolve<DueParser>().Parse(textWithDue);

            // Assert
            Assert.IsNull(result);
        }
    }
}
