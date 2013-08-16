using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.CalendarSynchronization.Formatting.Formatters;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatting
{
    [TestFixture]
    public class CheckItemTimeFrameFormatterTests : TestsBase
    {
        [Test]
        public void returns_null_for_null_check_item()
        {
            // Act
            var result = AutoMoqer.Resolve<CheckItemTimeFrameFormatter>().Format(null, new User());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void returns_null_for_null_user()
        {
            // Act
            var result = AutoMoqer.Resolve<CheckItemTimeFrameFormatter>().Format(new CheckItem(), null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void returns_null_for_null_due()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew().Build())
                                    .Build();

            AutoMoqer.GetMock<IParser<Due>>()
                     .Setup(x => x.Parse(checkItem.Name, user.UserPreferences))
                     .Returns((Due)null);

            // Act
            var result = AutoMoqer.Resolve<CheckItemTimeFrameFormatter>().Format(checkItem, user);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void formats_non_whole_day_time_frame()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = true).Build();

            var user = Builder<User>.CreateNew().Build();

            var start = Builder<TimeStamp>.CreateNew().Build();
            var end = Builder<TimeStamp>.CreateNew().Build();

            MockUserContext(user);
            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, user.UserPreferences)).Returns(due);
            MockTimeFrameCreation_FromLocal(due.DueDateTime, user, start, end);

            // Act
            var result = AutoMoqer.Resolve<CheckItemTimeFrameFormatter>().Format(checkItem, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(start, result.Item1);
            Assert.AreSame(end, result.Item2);
        }

        [Test]
        public void formats_whole_day_time_frame()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();

            var user = Builder<User>.CreateNew().Build();

            var start = Builder<TimeStamp>.CreateNew().Build();
            var end = Builder<TimeStamp>.CreateNew().Build();

            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, null)).Returns(due);
            MockTimeFrameCreation_WholeDay(due.DueDateTime, start, end);

            // Act
            var result = AutoMoqer.Resolve<CheckItemTimeFrameFormatter>().Format(checkItem, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(start, result.Item1);
            Assert.AreSame(end, result.Item2);
        }

        private void MockTimeFrameCreation_FromLocal(DateTime dueDateTime, User user, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            var timeZone = user != null ? user.CalendarTimeZone : null;

            var wholeDayIndicator = user != null && user.UserPreferences != null
                                        ? user.UserPreferences.WholeDayEventDueTime
                                        : null;

            AutoMoqer.GetMock<IEventTimeFrameCreator>()
                     .Setup(x => x.CreateFromLocal(dueDateTime, timeZone, wholeDayIndicator))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(eventStart, eventEnd));
        }

        private void MockTimeFrameCreation_WholeDay(DateTime dueDateTime, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            AutoMoqer.GetMock<IEventTimeFrameCreator>()
                     .Setup(x => x.CreateWholeDayTimeFrame(dueDateTime))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(eventStart, eventEnd));
        }
    }
}
