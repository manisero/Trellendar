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
        public void sets_event_location___not_null_location()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var user = Builder<User>.CreateNew().With(x => x.UserPreferences = new UserPreferences()).Build();
            var expectedLocation = "location";

            var due = Builder<Due>.CreateNew().With(x => x.HasTime = true).Build();

            AutoMoqer.GetMock<IParser<Location>>()
                     .Setup(x => x.Parse(checkItem.Name, user.UserPreferences))
                     .Returns(new Location { Value = expectedLocation });

            MockUserContext(user);
            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, user.UserPreferences)).Returns(due);
            MockTimeFrameCreation_FromLocal(due.DueDateTime, user);

            // Act
            var result = TestProcess(checkItem, "not important", user);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(expectedLocation, result.Location);
        }

        [Test]
        public void sets_event_location___null_location()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var user = Builder<User>.CreateNew().With(x => x.UserPreferences = new UserPreferences()).Build();

            var due = Builder<Due>.CreateNew().With(x => x.HasTime = true).Build();

            AutoMoqer.GetMock<IParser<Location>>()
                     .Setup(x => x.Parse(checkItem.Name, user.UserPreferences))
                     .Returns((Location)null);

            MockUserContext(user);
            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, user.UserPreferences)).Returns(due);
            MockTimeFrameCreation_FromLocal(due.DueDateTime, user);

            // Act
            var result = TestProcess(checkItem, "not important", user);

            // Assert
            Assert.NotNull(result);
            Assert.IsNull(result.Location);
        }
    }
}
