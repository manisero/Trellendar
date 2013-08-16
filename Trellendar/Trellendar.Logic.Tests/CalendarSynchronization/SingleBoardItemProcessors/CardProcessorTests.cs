﻿using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatters;
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

            MockTimeFrameCreation_FromUTC(card.Due.Value, null);

            // Act
            var result = TestProcess(card, "not important", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ExtendedProperties);

            var properties = result.ExtendedProperties.Private;
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
