using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    [TestFixture]
    public class CardProcessorTests : SingleBoardItemProcessorTestsBase<CardProcessor, Card>
    {
        [Test]
        public void returns_null_for_closed_card()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().With(x => x.Closed = true).Build();

            // Act
            var result = TestProcess(card, "not important");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_user()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();

            // Act
            var result = TestProcess(card, "not important", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(card.Name, result.summary);
        }

        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_preferences()
        {
            // Arrange
            var user = Builder<User>.CreateNew().With(x => x.UserPreferences = null).Build();
            var card = Builder<Card>.CreateNew().Build();

            // Act
            var result = TestProcess(card, "not important", user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(card.Name, result.summary);
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

            var card = Builder<Card>.CreateNew().Build();

            // Act
            var result = TestProcess(card, "not important", user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(card.Name, result.summary);
        }

        [Test]
        public void formats_summary_for_null_preffered_event_name_template___not_null_template(
            [Values("{0}{1}", "[{0}] {1}", "{1} ({0})", "{1}")] string eventNameTemplate)
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew()
                                        .With(preferences => preferences.CardEventNameTemplate = eventNameTemplate)
                                        .Build())
                                    .Build();

            var listName = "list";
            var card = Builder<Card>.CreateNew().Build();

            var expectedSummary = string.Format(eventNameTemplate, listName, card.Name);

            // Act
            var result = TestProcess(card, listName, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSummary, result.summary);
        }

        protected override object GetExptectedItemKey(Card item)
        {
            return item.Id;
        }
    }
}
