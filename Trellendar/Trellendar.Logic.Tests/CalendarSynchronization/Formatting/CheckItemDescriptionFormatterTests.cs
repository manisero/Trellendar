using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatting
{
    [TestFixture]
    public class CheckItemDescriptionFormatterTests : TestsBase
    {
        [Test]
        [Sequential]
        public void formats_with_card_summary_and_url(
            [Values("summary", "card summary")] string cardSummary,
            [Values("url", "http://card.url")] string cardUrl,
            [Values("Card: summary\nLink: url", "Card: card summary\nLink: http://card.url")]string expectedDescription
            )
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                            .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                .With(checkList => checkList.Card = Builder<Card>.CreateNew()
                                    .With(card => card.Url = cardUrl)
                                    .Build())
                                .Build())
                            .Build();

            var preferences = Builder<UserPreferences>.CreateNew().Build();

            // Arrange, Act & Assert
            TestFormat(checkItem, preferences, cardSummary, expectedDescription);
        }

        [Test]
        [Sequential]
        public void formats_without_card_summary(
            [Values("url", "http://card.url")] string cardUrl,
            [Values("Link: url", "Link: http://card.url")]string expectedDescription)
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                            .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                .With(checkList => checkList.Card = Builder<Card>.CreateNew()
                                    .With(card => card.Url = cardUrl)
                                    .Build())
                                .Build())
                            .Build();

            var preferences = Builder<UserPreferences>.CreateNew().Build();

            // Arrange, Act & Assert
            TestFormat(checkItem, preferences, null, expectedDescription);
        }

        [Test]
        [Sequential]
        public void formats_without_card_url(
            [Values("summary", "card summary")] string cardSummary,
            [Values("Card: summary", "Card: card summary")]string expectedDescription
            )
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                            .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                .With(checkList => checkList.Card = Builder<Card>.CreateNew()
                                    .With(card => card.Url = null)
                                    .Build())
                                .Build())
                            .Build();

            var preferences = Builder<UserPreferences>.CreateNew().Build();

            // Arrange, Act & Assert
            TestFormat(checkItem, preferences, cardSummary, expectedDescription);
        }

        [Test]
        [Sequential]
        public void formats_without_card_summary_and_url()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                            .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                .With(checkList => checkList.Card = Builder<Card>.CreateNew()
                                    .With(card => card.Url = null)
                                    .Build())
                                .Build())
                            .Build();

            var preferences = Builder<UserPreferences>.CreateNew().Build();

            // Arrange, Act & Assert
            TestFormat(checkItem, preferences, null, null);
        }

        [Test]
        [Sequential]
        public void formats_without_card()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                            .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                .With(checkList => checkList.Card = null)
                                .Build())
                            .Build();

            // Arrange, Act & Assert
            TestFormat(checkItem, null, null, null);
        }

        [Test]
        [Sequential]
        public void formats_without_checklist()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().With(x => x.CheckList = null).Build();

            // Arrange, Act & Assert
            TestFormat(checkItem, null, null, null);
        }

        private void TestFormat(CheckItem checkItem, UserPreferences preferences, string cardSummary, string expectedDescription)
        {
            // Arrange
            if (checkItem.CheckList != null)
            {
                AutoMoqer.GetMock<ISummaryFormatter<Card>>().Setup(x => x.Format(checkItem.CheckList.Card, preferences)).Returns(cardSummary);
            }

            // Act
            var result = AutoMoqer.Resolve<CheckItemDescriptionFormatter>().Format(checkItem, preferences);

            // Assert
            Assert.AreEqual(expectedDescription, result);
        }
    }
}
