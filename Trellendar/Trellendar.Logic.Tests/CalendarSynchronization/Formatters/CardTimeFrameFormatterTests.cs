using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.CalendarSynchronization.Formatters._Impl;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatters
{
    [TestFixture]
    public class CardTimeFrameFormatterTests : TestsBase
    {
        [Test]
        public void returns_null_for_null_card()
        {
            // Act
            var result = AutoMoqer.Resolve<CardTimeFrameFormatter>().Format(null, new User());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void returns_null_for_null_user()
        {
            // Act
            var result = AutoMoqer.Resolve<CardTimeFrameFormatter>().Format(new Card(), null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void formats_time_frame___null_calendar_time_zone()
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
        public void formats_time_frame___null_user_preferences()
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = null)
                                    .Build();

            // Act & Assert
            TestEventTimeFrameSetting(user);
        }

        [Test]
        public void formats_time_frame___null_preferred_WholeDayEventDueTime()
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
        public void formats_time_frame_from_parsed_due___non_whole_day()
        {
            // Arrange
            var user = Builder<User>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = true).Build();

            // Act & Assert
            TestEventTimeFrameSetting(user, due);
        }

        [Test]
        public void formats_time_frame_from_parsed_due___whole_day()
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
                AutoMoqer.GetMock<IParser<Due>>()
                         .Setup(x => x.Parse(card.Description, user != null ? user.UserPreferences : null))
                         .Returns(parsedDue);
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
            var result = AutoMoqer.Resolve<CardTimeFrameFormatter>().Format(card, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(start, result.Item1);
            Assert.AreSame(end, result.Item2);
        }

        private void MockTimeFrameCreation_FromUTC(DateTime dueDateTime, User user, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            var timeZone = user != null ? user.CalendarTimeZone : null;

            var wholeDayIndicator = user != null && user.UserPreferences != null
                                        ? user.UserPreferences.WholeDayEventDueTime
                                        : null;

            AutoMoqer.GetMock<IEventTimeFrameCreator>()
                     .Setup(x => x.CreateFromUTC(dueDateTime, timeZone, wholeDayIndicator))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(eventStart, eventEnd));
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
