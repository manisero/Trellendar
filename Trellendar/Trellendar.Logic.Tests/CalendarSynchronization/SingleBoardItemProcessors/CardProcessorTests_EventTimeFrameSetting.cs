using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    public partial class CardProcessorTests
    {
        [Test]
        public void sets_event_start_and_end_properly___null_user()
        {
            TestEventTimeFrameSetting(null);
        }

        [Test]
        public void sets_event_start_and_end_properly___null_calendar_time_zone()
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.CalendarTimeZone = null)
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew().Build())
                                    .Build();

            // Act & Assert
            TestEventTimeFrameSetting(user);
        }

        [Test]
        public void sets_event_start_and_end_properly___null_user_preferences()
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = null)
                                    .Build();

            // Act & Assert
            TestEventTimeFrameSetting(user);
        }

        [Test]
        public void sets_event_start_and_end_properly___null_preferred_WholeDayEventDueTime()
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew()
                                        .With(preferences => preferences.WholeDayEventDueTime = null)
                                        .Build())
                                    .Build();

            // Act & Assert
            TestEventTimeFrameSetting(user);
        }

        private void TestEventTimeFrameSetting(User user)
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();

            var start = new TimeStamp();
            var end = new TimeStamp();

            MockEventTimeFrameCreator(card.Due.Value, user, start, end);

            // Act
            var result = TestProcess(card, "not important", user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(start, result.Start);
            Assert.AreSame(end, result.End);
        }
    }
}
