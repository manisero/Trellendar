using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;

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

        [Test]
        public void sets_event_start_and_end_from_parsed_due___non_whole_day()
        {
            // Arrange
            var user = Builder<User>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = true).Build();

            // Act & Assert
            TestEventTimeFrameSetting(user, due);
        }

        [Test]
        public void sets_event_start_and_end_from_parsed_due___whole_day()
        {
            // Arrange
            var user = Builder<User>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();

            // Act & Assert
            TestEventTimeFrameSetting(user, due);
        }

        private void TestEventTimeFrameSetting(User user, Due parsedDue = null)
        {
            // Arrange
            var card = Builder<Card>.CreateNew()
                                    .With(x => x.Due = parsedDue == null ? new DateTime(2012, 07, 8) : (DateTime?)null)
                                    .Build();

            var start = new TimeStamp();
            var end = new TimeStamp();

            if (parsedDue != null)
            {
                AutoMoqer.GetMock<IDueParser>().Setup(x => x.Parse(card.Desc)).Returns(parsedDue);
            }

            if (parsedDue == null)
            {
                MockTimeFrameCreation_FromUTC(card.Due.Value, user, start, end);
            }
            else if (parsedDue.HasTime)
            {
                MockTimeFrameCreation_FromLocal(parsedDue.DueDateTime, user, start, end);
            }
            else
            {
                MockTimeFrameCreation_WholeDay(parsedDue.DueDateTime, start, end);
            }

            // Act
            var result = TestProcess(card, "not important", user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(start, result.Start);
            Assert.AreSame(end, result.End);
        }
    }
}
