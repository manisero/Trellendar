using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    public partial class CardProcessorTests
    {
        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_user()
        {
            // Arrange
            var cardName = "card name";

            // Arrange, Act & Assert
            TestFormatEventSummary(null, "not important", "not important", cardName, cardName);
        }

        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_preferences()
        {
            // Arrange
            var user = Builder<User>.CreateNew().With(x => x.UserPreferences = null).Build();

            var cardName = "card name";

            // Arrange, Act & Assert
            TestFormatEventSummary(user, "not important", "not important", cardName, cardName);
        }

        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_template()
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew()
                                        .With(preferences => preferences.CardEventNameTemplate = null)
                                        .Build())
                                    .Build();

            var cardName = "card name";

            // Arrange, Act & Assert
            TestFormatEventSummary(user, "not important", "not important", cardName, cardName);
        }

        [Test]
        [Sequential]
        public void formats_summary_for_not_null_preffered_event_name_template(
            [Values("list name", "list name")] string listName,
            [Values("shortcut", "shortcut")] string listShortcut,
            [Values("card name", "card name")] string cardName,
            [Values("{0} {1}", "[{0}] {1}")] string eventNameTemplate,
            [Values("shortcut card name", "[shortcut] card name")] string expectedSummary)
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew()
                                        .With(preferences => preferences.CardEventNameTemplate = eventNameTemplate)
                                        .Build())
                                    .Build();

            // Arrange, Act & Assert
            TestFormatEventSummary(user, listName, listShortcut, cardName, expectedSummary);
        }

        private void TestFormatEventSummary(User user, string listName, string listShortcut, string cardName, string expectedSummary)
        {
            // Arrange
            var card = Builder<Card>.CreateNew().With(x => x.Name = cardName).Build();

            AutoMoqer.GetMock<IParser<BoardItemName>>()
                     .Setup(x => x.Parse(listName, user != null ? user.UserPreferences : null))
                     .Returns(new BoardItemName { Value = listShortcut });

            MockTimeFrameCreation_FromUTC(card.Due.Value, user);

            // Act
            var result = TestProcess(card, listName, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSummary, result.Summary);
        }
    }
}
