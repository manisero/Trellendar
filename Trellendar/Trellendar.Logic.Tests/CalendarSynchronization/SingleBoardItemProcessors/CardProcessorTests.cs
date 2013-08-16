using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatters;
using Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    [TestFixture]
    public partial class CardProcessorTests : SingleBoardItemProcessorTestsBase<CardProcessor, Card>
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
        public void sets_event_summary_properly()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var summary = "summary";

            AutoMoqer.GetMock<ICardSummaryFormatter>().Setup(x => x.Format(card, preferences)).Returns(summary);

            MockTimeFrameCreation_FromUTC(card.Due.Value, null);

            // Act
            var result = TestProcess(card, "not important", new User { UserPreferences = preferences });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(summary, result.Summary);
        }

        [Test]
        public void sets_event_description_properly()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var description = "description";

            AutoMoqer.GetMock<ICardDescriptionFormatter>().Setup(x => x.Format(card)).Returns(description);

            MockTimeFrameCreation_FromUTC(card.Due.Value, null);

            // Act
            var result = TestProcess(card, "not important", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(description, result.Description);
        }

        [Test]
        public void sets_event_extended_properties_properly()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var extendedProperties = Builder<EventExtendedProperties>.CreateNew().Build();

            AutoMoqer.GetMock<ICardExtendedPropertiesFormatter>().Setup(x => x.Format(card)).Returns(extendedProperties);
            MockTimeFrameCreation_FromUTC(card.Due.Value, null);

            // Act
            var result = TestProcess(card, "not important", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(extendedProperties, result.ExtendedProperties);
        }

        protected override object GetExptectedItemKey(Card item)
        {
            return item.Id;
        }
    }
}
