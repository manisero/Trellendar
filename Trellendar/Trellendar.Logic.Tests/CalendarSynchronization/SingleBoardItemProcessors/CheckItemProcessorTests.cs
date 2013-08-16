using System;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatting;
using Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    [TestFixture]
    public class CheckItemProcessorTests : SingleBoardItemProcessorTestsBase<CheckItemProcessor, CheckItem>
    {
        [Test]
        public void returns_null_for_closed_card()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                                              .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                                  .With(checkList => checkList.Card = Builder<Card>.CreateNew()
                                                      .With(card => card.Closed = true)
                                                      .Build())
                                                  .Build())
                                              .Build();

            var user = Builder<User>.CreateNew().Build();

            // Act
            var result = TestProcess(checkItem, user);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void returns_null_for_null_time_frame()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                                .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                    .With(checkList => checkList.Card = Builder<Card>.CreateNew().Build())
                                    .Build())
                                .Build();

            var user = Builder<User>.CreateNew().Build();

            AutoMoqer.GetMock<ITimeFrameFormatter<CheckItem>>().Setup(x => x.Format(checkItem, user)).Returns((Tuple<TimeStamp, TimeStamp>)null);

            // Act
            var result = TestProcess(checkItem, user);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void sets_event_start_and_end()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                                .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                    .With(checkList => checkList.Card = Builder<Card>.CreateNew().Build())
                                    .Build())
                                .Build();

            var user = Builder<User>.CreateNew().Build();
            var timeFrame = new Tuple<TimeStamp, TimeStamp>(new TimeStamp(), new TimeStamp());
            
            AutoMoqer.GetMock<ITimeFrameFormatter<CheckItem>>().Setup(x => x.Format(checkItem, user)).Returns(timeFrame);

            // Act
            var result = TestProcess(checkItem, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(timeFrame.Item1, result.Start);
            Assert.AreSame(timeFrame.Item2, result.End);
        }

        [Test]
        public void sets_event_summary()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                                .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                    .With(checkList => checkList.Card = Builder<Card>.CreateNew().Build())
                                    .Build())
                                .Build();

            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var summary = "summary";

            AutoMoqer.GetMock<ISummaryFormatter<CheckItem>>().Setup(x => x.Format(checkItem, preferences)).Returns(summary);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(checkItem, new User { UserPreferences = preferences });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(summary, result.Summary);
        }

        [Test]
        public void sets_event_location()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                                .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                    .With(checkList => checkList.Card = Builder<Card>.CreateNew().Build())
                                    .Build())
                                .Build();

            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var location = "location";

            AutoMoqer.GetMock<ILocationFormatter<CheckItem>>().Setup(x => x.Format(checkItem, preferences)).Returns(location);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(checkItem, new User { UserPreferences = preferences });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(location, result.Location);
        }

        [Test]
        public void sets_event_description()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                                .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                    .With(checkList => checkList.Card = Builder<Card>.CreateNew().Build())
                                    .Build())
                                .Build();

            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var descritpion = "descritpion";

            AutoMoqer.GetMock<IDescriptionFormatter<CheckItem>>().Setup(x => x.Format(checkItem, preferences)).Returns(descritpion);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(checkItem, new User { UserPreferences = preferences });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(descritpion, result.Description);
        }

        [Test]
        public void sets_event_extended_properties()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                                .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                    .With(checkList => checkList.Card = Builder<Card>.CreateNew().Build())
                                    .Build())
                                .Build();

            var extendedProperties = Builder<EventExtendedProperties>.CreateNew().Build();

            AutoMoqer.GetMock<IExtendedPropertiesFormatter<CheckItem>>().Setup(x => x.Format(checkItem)).Returns(extendedProperties);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(checkItem, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(extendedProperties, result.ExtendedProperties);
        }

        protected override object GetExptectedItemKey(CheckItem item)
        {
            return item.Id;
        }

        private void MockTimeFrameFormatting()
        {
            AutoMoqer.GetMock<ITimeFrameFormatter<CheckItem>>()
                     .Setup(x => x.Format(It.IsAny<CheckItem>(), It.IsAny<User>()))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(null, null));
        }
    }
}
