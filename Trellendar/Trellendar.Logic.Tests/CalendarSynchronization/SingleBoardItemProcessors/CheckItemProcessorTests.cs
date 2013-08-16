using System;
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
    public partial class CheckItemProcessorTests : SingleBoardItemProcessorTestsBase<CheckItemProcessor, CheckItem>
    {
        [Test]
        public void returns_null_for_null_time_frame()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var user = Builder<User>.CreateNew().Build();

            AutoMoqer.GetMock<ICheckItemTimeFrameFormatter>().Setup(x => x.Format(checkItem, user)).Returns((Tuple<TimeStamp, TimeStamp>)null);

            // Act
            var result = TestProcess(checkItem, "not important", user);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void sets_event_start_and_end()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var user = Builder<User>.CreateNew().Build();
            var timeFrame = new Tuple<TimeStamp, TimeStamp>(new TimeStamp(), new TimeStamp());
            
            AutoMoqer.GetMock<ICheckItemTimeFrameFormatter>().Setup(x => x.Format(checkItem, user)).Returns(timeFrame);

            // Act
            var result = TestProcess(checkItem, "not important", user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(timeFrame.Item1, result.Start);
            Assert.AreSame(timeFrame.Item2, result.End);
        }

        [Test]
        public void sets_event_summary()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var summary = "summary";

            AutoMoqer.GetMock<ICheckItemSummaryFormatter>().Setup(x => x.Format(checkItem, preferences)).Returns(summary);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(checkItem, "not important", new User { UserPreferences = preferences });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(summary, result.Summary);
        }

        [Test]
        public void sets_event_description()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var descritpion = "descritpion";
            
            AutoMoqer.GetMock<ICheckItemDescriptionFormatter>().Setup(x => x.Format(checkItem, preferences)).Returns(descritpion);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(checkItem, "not important", new User { UserPreferences = preferences });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(descritpion, result.Description);
        }

        [Test]
        public void sets_event_extended_properties()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var extendedProperties = Builder<EventExtendedProperties>.CreateNew().Build();

            AutoMoqer.GetMock<ICheckItemExtendedPropertiesFormatter>().Setup(x => x.Format(checkItem)).Returns(extendedProperties);
            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(checkItem, "not important", null);

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
            AutoMoqer.GetMock<ICheckItemTimeFrameFormatter>()
                     .Setup(x => x.Format(It.IsAny<CheckItem>(), It.IsAny<User>()))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(null, null));
        }
    }
}
