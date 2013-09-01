using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Parsers;

namespace Trellendar.Logic.Tests.Synchronization.CalendarSynchronization.Parsers
{
    [TestFixture]
    public class BoardItemNameParserTests : TestsBase
    {
        [Test]
        [Sequential]
        public void parses_name_without_shortcut(
            [Values("{", "<shortcut>")] string beginningMarker,
            [Values("}", "</shortcut>")] string endMarker,
            [Values("text text",
                    "text text text")] 
                    string text,
            [Values("text text", "text text text")] string expectedName)
        {
            // Arrange
            var settings = Builder<BoardCalendarBondSettings>.CreateNew()
                                                             .With(x => x.TrelloItemShortcutBeginningMarker = beginningMarker)
                                                             .With(x => x.TrelloItemShortcutEndMarker = endMarker)
                                                             .Build();

            // Act
            var result = AutoMoqer.Resolve<BoardItemNameParser>().Parse(text, settings);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedName, result.Value);
        }

        [Test]
        [Sequential]
        public void parses_name_with_shortcut(
            [Values("{", "<shortcut>", "[shortcut]")] string beginningMarker,
            [Values("}", "</shortcut>", "[endofshortcut]")] string endMarker,
            [Values("text {shortcut} text",
                    "text <shortcut>shortcut</shortcut> text",
                    "text [shortcut]other shortcut[endofshortcut] text")] 
                    string text,
            [Values("shortcut", "shortcut", "other shortcut")] string expectedName)
        {
            // Arrange
            var settings = Builder<BoardCalendarBondSettings>.CreateNew()
                                                             .With(x => x.TrelloItemShortcutBeginningMarker = beginningMarker)
                                                             .With(x => x.TrelloItemShortcutEndMarker = endMarker)
                                                             .Build();

            // Act
            var result = AutoMoqer.Resolve<BoardItemNameParser>().Parse(text, settings);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedName, result.Value);
        }

        [Test]
        [Sequential]
        public void handles_multiple_markers_in_text(
            [Values("{", "<shortcut>")] string beginningMarker,
            [Values("}", "</shortcut>")] string endMarker,
            [Values("text {shortcut1} text {shortcut2} text",
                    "text <shortcut>shortcut1</shortcut> text <shortcut>shortcut2</shortcut> text")]
                    string text,
            [Values("shortcut1", "shortcut1")] string expectedName)
        {
            // Arrange
            var settings = Builder<BoardCalendarBondSettings>.CreateNew()
                                                             .With(x => x.TrelloItemShortcutBeginningMarker = beginningMarker)
                                                             .With(x => x.TrelloItemShortcutEndMarker = endMarker)
                                                             .Build();

            // Act
            var result = AutoMoqer.Resolve<BoardItemNameParser>().Parse(text, settings);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedName, result.Value);
        }
    }
}
