using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Synchronization.CalendarSynchronization;

namespace Trellendar.Logic.Tests.Synchronization.CalendarSynchronization.SingleBoardItemProcessors
{
    [TestFixture]
    public abstract class SingleBoardItemProcessorTestsBase<TProcessor, TItem> : TestsBase
        where TProcessor : ISingleBoardItemProcessor<TItem>
    {
        [SetUp]
        public void TestSetUp()
        {
            MockBoardCalendarContext((BoardCalendarBond)null);
        }

        [Test]
        public void returns_proper_item_id()
        {
            // Arrange
            var item = Builder<TItem>.CreateNew().Build();
            var itemId = GetExptectedItemKey(item);

            // Act
            var result = AutoMoqer.Resolve<TProcessor>().GetItemID(item);

            // Assert
            Assert.AreEqual(itemId, result);
        }

        protected abstract object GetExptectedItemKey(TItem item);

        protected void MockTimeFrameCreation_FromUTC(DateTime dueDateTime, BoardCalendarBond boardCalendarBond, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            var timeZone = boardCalendarBond != null ? boardCalendarBond.CalendarTimeZone : null;

            var wholeDayIndicator = boardCalendarBond != null && boardCalendarBond.Settings != null
                                        ? boardCalendarBond.Settings.WholeDayEventDueTime
                                        : null;

            AutoMoqer.GetMock<IEventTimeFrameCreator>()
                     .Setup(x => x.CreateFromUTC(dueDateTime, timeZone, wholeDayIndicator))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(eventStart, eventEnd));
        }

        protected void MockTimeFrameCreation_FromLocal(DateTime dueDateTime, BoardCalendarBond boardCalendarBond, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            var timeZone = boardCalendarBond != null ? boardCalendarBond.CalendarTimeZone : null;

            var wholeDayIndicator = boardCalendarBond != null && boardCalendarBond.Settings != null
                                        ? boardCalendarBond.Settings.WholeDayEventDueTime
                                        : null;

            AutoMoqer.GetMock<IEventTimeFrameCreator>()
                     .Setup(x => x.CreateFromLocal(dueDateTime, timeZone, wholeDayIndicator))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(eventStart, eventEnd));
        }

        protected void MockTimeFrameCreation_WholeDay(DateTime dueDateTime, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            AutoMoqer.GetMock<IEventTimeFrameCreator>()
                     .Setup(x => x.CreateWholeDayTimeFrame(dueDateTime))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(eventStart, eventEnd));
        }

        protected Event TestProcess(TItem item)
        {
            // Act
            return AutoMoqer.Resolve<TProcessor>().Process(item);
        }

        protected Event TestProcess(TItem item, BoardCalendarBond boardCalendarBond)
        {
            // Arrange
            AutoMoqer.SetInstance(new BoardCalendarContext(boardCalendarBond));

            // Act
            return AutoMoqer.Resolve<TProcessor>().Process(item);
        }
    }
}
