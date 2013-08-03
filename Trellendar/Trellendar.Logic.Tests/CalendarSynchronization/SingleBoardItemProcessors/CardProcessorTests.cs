using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors;
using Trellendar.Logic.Domain;

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
        public void sets_event_extended_properties_properly()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();

            MockEventTimeFrameCreator(card.Due.Value, null);

            // Act
            var result = TestProcess(card, "not important", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.extendedProperties);

            var properties = result.extendedProperties.@private;
            Assert.IsNotNull(properties);
            Assert.IsTrue(properties.ContainsKey(EventExtensions.GENERATED_PROPERTY_KEY));
            Assert.IsTrue(properties.ContainsKey(EventExtensions.SOURCE_ID_PROPERTY_KEY));
            Assert.AreEqual(card.Id, properties[EventExtensions.SOURCE_ID_PROPERTY_KEY]);
        }

        protected override object GetExptectedItemKey(Card item)
        {
            return item.Id;
        }
    }
}
