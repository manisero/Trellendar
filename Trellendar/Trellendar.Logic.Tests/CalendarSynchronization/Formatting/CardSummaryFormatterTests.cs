using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.CalendarSynchronization.Formatting.Formatters;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatting
{
    [TestFixture]
    public class CardSummaryFormatterTests : TestsBase
    {
        [Test]
        [Sequential]
        public void formats_summary_for_not_null_preffered_event_name_template___not_null_list(
            [Values("shortcut", "shortcut")] string listShortcut,
            [Values("card name", "card name")] string cardName,
            [Values("{0} {1}", "[{0}] {1}")] string eventNameTemplate,
            [Values("shortcut card name", "[shortcut] card name")] string expectedSummary)
        {
            // Arrange
            var card = Builder<Card>.CreateNew()
                                    .With(x => x.Name = cardName)
                                    .With(x => x.List = Builder<Trellendar.Domain.Trello.List>.CreateNew().Build())
                                    .Build();

            var preferences = Builder<UserPreferences>.CreateNew()
                                                      .With(x => x.CardEventNameTemplate = eventNameTemplate)
                                                      .Build();

            // Arrange, Act & Assert
            TestFormat(card, preferences, listShortcut, expectedSummary);
        }

        [Test]
        public void formats_summary_for_null_list()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().With(x => x.List = null).Build();

            // Arrange, Act & Assert
            TestFormat(card, null, null, card.Name);
        }

        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_preferences()
        {
            // Arrange
            var card = Builder<Card>.CreateNew()
                                    .With(x => x.List = Builder<Trellendar.Domain.Trello.List>.CreateNew().Build())
                                    .Build();

            // Arrange, Act & Assert
            TestFormat(card, null, null, card.Name);
        }

        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_template()
        {
            // Arrange
            var card = Builder<Card>.CreateNew()
                                    .With(x => x.List = Builder<Trellendar.Domain.Trello.List>.CreateNew().Build())
                                    .Build();

            var preferences = Builder<UserPreferences>.CreateNew().With(x => x.CardEventNameTemplate = null).Build();

            // Arrange, Act & Assert
            TestFormat(card, preferences, null, card.Name);
        }

        [Test]
        [Sequential]
        public void formats_summary_for_not_null_preffered_event_name_template___null_list()
        {
            // Arrange
            var card = Builder<Card>.CreateNew()
                                    .With(x => x.List = null)
                                    .Build();

            var preferences = Builder<UserPreferences>.CreateNew().Build();

            // Arrange, Act & Assert
            TestFormat(card, preferences, null, card.Name);
        }

        private void TestFormat(Card card, UserPreferences preferences, string listShortcut, string expectedSummary)
        {
            // Arrange
            if (listShortcut != null)
            {
                AutoMoqer.GetMock<IParser<BoardItemName>>()
                         .Setup(x => x.Parse(card.List.Name, preferences))
                         .Returns(new BoardItemName { Value = listShortcut });
            }

            // Act
            var result = AutoMoqer.Resolve<CardSummaryFormatter>().Format(card, preferences);

            // Assert
            Assert.AreEqual(expectedSummary, result);
        }
    }
}
