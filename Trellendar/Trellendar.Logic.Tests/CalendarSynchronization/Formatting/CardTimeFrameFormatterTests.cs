using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Logic.Synchronization.CalendarSynchronization;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatting
{
    [TestFixture]
    public class CardTimeFrameFormatterTests : TestsBase
    {
        [Test]
        public void returns_null_for_null_card()
        {
            // Act
            var result = AutoMoqer.Resolve<CardTimeFrameFormatter>().Format(null, new BoardCalendarBond());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void returns_null_for_null_bond()
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
            var bond = Builder<BoardCalendarBond>.CreateNew()
                                                 .With(x => x.CalendarTimeZone = null)
                                                 .With(x => x.Settings = Builder<BoardCalendarBondSettings>.CreateNew().Build())
                                                 .Build();

            // Act & Assert
            TestEventTimeFrameSetting(bond);
        }

        [Test]
        public void formats_time_frame___null_bond_settings()
        {
            // Arrange
            var bond = Builder<BoardCalendarBond>.CreateNew()
                                                 .With(x => x.Settings = null)
                                                 .Build();

            // Act & Assert
            TestEventTimeFrameSetting(bond);
        }

        [Test]
        public void formats_time_frame___null_WholeDayEventDueTime_setting()
        {
            // Arrange
            var bond = Builder<BoardCalendarBond>.CreateNew()
                                                 .With(x => x.Settings = Builder<BoardCalendarBondSettings>.CreateNew()
                                                     .With(preferences => preferences.WholeDayEventDueTime = null)
                                                     .Build())
                                                 .Build();

            // Act & Assert
            TestEventTimeFrameSetting(bond);
        }

        [Test]
        public void formats_time_frame_from_parsed_due___non_whole_day()
        {
            // Arrange
            var bond = Builder<BoardCalendarBond>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = true).Build();

            // Act & Assert
            TestEventTimeFrameSetting(bond, due);
        }

        [Test]
        public void formats_time_frame_from_parsed_due___whole_day()
        {
            // Arrange
            var bond = Builder<BoardCalendarBond>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();

            // Act & Assert
            TestEventTimeFrameSetting(bond, due);
        }

        private void TestEventTimeFrameSetting(BoardCalendarBond boardCalendarBond, Due parsedDue = null)
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
                         .Setup(x => x.Parse(card.Description, boardCalendarBond != null ? boardCalendarBond.Settings : null))
                         .Returns(parsedDue);
            }

            if (parsedDue == null)
            {
                MockTimeFrameCreation_FromUTC(card.Due.Value, boardCalendarBond, start, end);
            }
            else if (parsedDue.HasTime)
            {
                MockTimeFrameCreation_FromLocal(parsedDue.DueDateTime, boardCalendarBond, start, end);
            }
            else
            {
                MockTimeFrameCreation_WholeDay(parsedDue.DueDateTime, start, end);
            }

            // Act
            var result = AutoMoqer.Resolve<CardTimeFrameFormatter>().Format(card, boardCalendarBond);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(start, result.Item1);
            Assert.AreSame(end, result.Item2);
        }

        private void MockTimeFrameCreation_FromUTC(DateTime dueDateTime, BoardCalendarBond boardCalendarBond, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            var timeZone = boardCalendarBond != null ? boardCalendarBond.CalendarTimeZone : null;

            var wholeDayIndicator = boardCalendarBond != null && boardCalendarBond.Settings != null
                                        ? boardCalendarBond.Settings.WholeDayEventDueTime
                                        : null;

            AutoMoqer.GetMock<IEventTimeFrameCreator>()
                     .Setup(x => x.CreateFromUTC(dueDateTime, timeZone, wholeDayIndicator))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(eventStart, eventEnd));
        }

        private void MockTimeFrameCreation_FromLocal(DateTime dueDateTime, BoardCalendarBond boardCalendarBond, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            var timeZone = boardCalendarBond != null ? boardCalendarBond.CalendarTimeZone : null;

            var wholeDayIndicator = boardCalendarBond != null && boardCalendarBond.Settings != null
                                        ? boardCalendarBond.Settings.WholeDayEventDueTime
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
