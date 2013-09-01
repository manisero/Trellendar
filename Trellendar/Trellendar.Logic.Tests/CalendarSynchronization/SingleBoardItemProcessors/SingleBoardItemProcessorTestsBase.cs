using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.CalendarSynchronization;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    [TestFixture]
    public abstract class SingleBoardItemProcessorTestsBase<TProcessor, TItem> : TestsBase
        where TProcessor : ISingleBoardItemProcessor<TItem>
    {
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

        protected void MockTimeFrameCreation_FromUTC(DateTime dueDateTime, User user, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            var timeZone = user != null ? user.CalendarTimeZone : null;

            var wholeDayIndicator = user != null && user.UserPreferences != null
                                        ? user.UserPreferences.WholeDayEventDueTime
                                        : null;

            AutoMoqer.GetMock<IEventTimeFrameCreator>()
                     .Setup(x => x.CreateFromUTC(dueDateTime, timeZone, wholeDayIndicator))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(eventStart, eventEnd));
        }

        protected void MockTimeFrameCreation_FromLocal(DateTime dueDateTime, User user, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            var timeZone = user != null ? user.CalendarTimeZone : null;

            var wholeDayIndicator = user != null && user.UserPreferences != null
                                        ? user.UserPreferences.WholeDayEventDueTime
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

        protected Event TestProcess(TItem item, User user)
        {
            // Arrange
            AutoMoqer.SetInstance(new UserContext(user));

            // Act
            return AutoMoqer.Resolve<TProcessor>().Process(item);
        }
    }
}
