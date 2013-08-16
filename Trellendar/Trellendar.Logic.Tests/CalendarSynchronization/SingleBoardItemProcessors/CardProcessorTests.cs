﻿using System;
using FizzWare.NBuilder;
using Moq;
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
        public void returns_null_for_null_time_frame()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var user = Builder<User>.CreateNew().Build();

            AutoMoqer.GetMock<ICardTimeFrameFormatter>().Setup(x => x.Format(card, user)).Returns((Tuple<TimeStamp, TimeStamp>)null);

            // Act
            var result = TestProcess(card, "not important");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void sets_event_start_and_end()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var user = Builder<User>.CreateNew().Build();
            var timeFrame = Tuple.Create(new TimeStamp(), new TimeStamp());

            AutoMoqer.GetMock<ICardTimeFrameFormatter>().Setup(x => x.Format(card, user)).Returns(timeFrame);

            // Act
            var result = TestProcess(card, "not important", user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(timeFrame.Item1, result.Start);
            Assert.AreSame(timeFrame.Item2, result.End);
        }

        [Test]
        public void sets_event_summary()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var summary = "summary";

            AutoMoqer.GetMock<ICardSummaryFormatter>().Setup(x => x.Format(card, preferences)).Returns(summary);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(card, "not important", new User { UserPreferences = preferences });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(summary, result.Summary);
        }

        [Test]
        public void sets_event_description()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var description = "description";

            AutoMoqer.GetMock<ICardDescriptionFormatter>().Setup(x => x.Format(card)).Returns(description);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(card, "not important", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(description, result.Description);
        }

        [Test]
        public void sets_event_extended_properties()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var extendedProperties = Builder<EventExtendedProperties>.CreateNew().Build();

            AutoMoqer.GetMock<ICardExtendedPropertiesFormatter>().Setup(x => x.Format(card)).Returns(extendedProperties);
            MockTimeFrameFormatting();

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

        private void MockTimeFrameFormatting()
        {
            AutoMoqer.GetMock<ICardTimeFrameFormatter>()
                     .Setup(x => x.Format(It.IsAny<Card>(), It.IsAny<User>()))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(null, null));
        }
    }
}
