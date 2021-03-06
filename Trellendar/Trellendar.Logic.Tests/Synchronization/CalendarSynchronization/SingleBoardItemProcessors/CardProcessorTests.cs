﻿using System;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting;
using Trellendar.Logic.Synchronization.CalendarSynchronization.SingleBoardItemProcessors;

namespace Trellendar.Logic.Tests.Synchronization.CalendarSynchronization.SingleBoardItemProcessors
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
            var result = TestProcess(card);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void returns_null_for_null_time_frame()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var bond = Builder<BoardCalendarBond>.CreateNew().Build();

            AutoMoqer.GetMock<ITimeFrameFormatter<Card>>().Setup(x => x.Format(card, bond)).Returns((Tuple<TimeStamp, TimeStamp>)null);

            // Act
            var result = TestProcess(card);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void sets_event_start_and_end()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var bond = Builder<BoardCalendarBond>.CreateNew().Build();
            var timeFrame = Tuple.Create(new TimeStamp(), new TimeStamp());

            AutoMoqer.GetMock<ITimeFrameFormatter<Card>>().Setup(x => x.Format(card, bond)).Returns(timeFrame);

            // Act
            var result = TestProcess(card, bond);

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
            var settings = Builder<BoardCalendarBondSettings>.CreateNew().Build();
            var summary = "summary";

            AutoMoqer.GetMock<ISummaryFormatter<Card>>().Setup(x => x.Format(card, settings)).Returns(summary);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(card, new BoardCalendarBond { Settings = settings });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(summary, result.Summary);
        }

        [Test]
        public void sets_event_locaton()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var settings = Builder<BoardCalendarBondSettings>.CreateNew().Build();
            var location = "location";

            AutoMoqer.GetMock<ILocationFormatter<Card>>().Setup(x => x.Format(card, settings)).Returns(location);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(card, new BoardCalendarBond { Settings = settings });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(location, result.Location);
        }

        [Test]
        public void sets_event_description()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var description = "description";

            AutoMoqer.GetMock<IDescriptionFormatter<Card>>().Setup(x => x.Format(card, null)).Returns(description);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(card, null);

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

            AutoMoqer.GetMock<IExtendedPropertiesFormatter<Card>>().Setup(x => x.Format(card)).Returns(extendedProperties);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(card, null);

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
            AutoMoqer.GetMock<ITimeFrameFormatter<Card>>()
                     .Setup(x => x.Format(It.IsAny<Card>(), It.IsAny<BoardCalendarBond>()))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(null, null));
        }
    }
}
