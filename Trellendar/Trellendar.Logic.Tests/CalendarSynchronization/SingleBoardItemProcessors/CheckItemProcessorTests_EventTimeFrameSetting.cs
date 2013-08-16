using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    public partial class CheckItemProcessorTests
    {
        [Test]
        public void properly_sets_non_whole_day_time_frame()
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
            var result = TestProcess(checkItem, "not important");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(start, result.Start);
            Assert.AreSame(end, result.End);
        }

        [Test]
        public void properly_sets_whole_day_time_frame()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();

            var start = Builder<TimeStamp>.CreateNew().Build();
            var end = Builder<TimeStamp>.CreateNew().Build();

            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, null)).Returns(due);
            MockTimeFrameCreation_WholeDay(due.DueDateTime, start, end);

            // Act
            var result = TestProcess(checkItem, "not important");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(start, result.Start);
            Assert.AreSame(end, result.End);
        }
    }
}
