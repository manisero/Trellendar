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
    public class CheckItemTimeFrameFormatterTests : TestsBase
    {
        [Test]
        public void returns_null_for_null_check_item()
        {
            // Act
            var result = AutoMoqer.Resolve<CheckItemTimeFrameFormatter>().Format(null, new BoardCalendarBond());

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void returns_null_for_null_bond()
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
            var bond = Builder<BoardCalendarBond>.CreateNew()
                                    .With(x => x.Settings = Builder<BoardCalendarBondSettings>.CreateNew().Build())
                                    .Build();

            AutoMoqer.GetMock<IParser<Due>>()
                     .Setup(x => x.Parse(checkItem.Name, bond.Settings))
                     .Returns((Due)null);

            // Act
            var result = AutoMoqer.Resolve<CheckItemTimeFrameFormatter>().Format(checkItem, bond);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void formats_non_whole_day_time_frame()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = true).Build();

            var bond = Builder<BoardCalendarBond>.CreateNew().Build();

            var start = Builder<TimeStamp>.CreateNew().Build();
            var end = Builder<TimeStamp>.CreateNew().Build();

            MockBoardCalendarContext(bond);
            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, bond.Settings)).Returns(due);
            MockTimeFrameCreation_FromLocal(due.DueDateTime, bond, start, end);

            // Act
            var result = AutoMoqer.Resolve<CheckItemTimeFrameFormatter>().Format(checkItem, bond);

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

            var bond = Builder<BoardCalendarBond>.CreateNew().Build();

            var start = Builder<TimeStamp>.CreateNew().Build();
            var end = Builder<TimeStamp>.CreateNew().Build();

            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, null)).Returns(due);
            MockTimeFrameCreation_WholeDay(due.DueDateTime, start, end);

            // Act
            var result = AutoMoqer.Resolve<CheckItemTimeFrameFormatter>().Format(checkItem, bond);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(start, result.Item1);
            Assert.AreSame(end, result.Item2);
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
