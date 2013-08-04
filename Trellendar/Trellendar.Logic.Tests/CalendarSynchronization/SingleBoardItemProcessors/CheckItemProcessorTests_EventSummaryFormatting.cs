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
        public void formats_not_done_event_summary()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().With(x => x.State = CheckItemExtensions.STATE_NOT_DONE).Build();

            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();

            AutoMoqer.GetMock<IDueParser>().Setup(x => x.Parse(checkItem.Name)).Returns(due);
            MockTimeFrameCreation_WholeDay(due.DueDateTime);

            // Act
            var result = TestProcess(checkItem, "not important");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(checkItem.Name, result.Summary);
        }

        [Test]
        public void formats_done_event_summary()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().With(x => x.State = CheckItemExtensions.STATE_DONE).Build();
            var preferences = Builder<UserPreferences>.CreateNew().Build();

            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();

            MockUserContext(preferences);

            AutoMoqer.GetMock<IDueParser>().Setup(x => x.Parse(checkItem.Name)).Returns(due);
            MockTimeFrameCreation_WholeDay(due.DueDateTime);

            // Act
            var result = TestProcess(checkItem, "not important");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(checkItem.Name + preferences.CheckListEventDoneSuffix, result.Summary);
        }
    }
}
