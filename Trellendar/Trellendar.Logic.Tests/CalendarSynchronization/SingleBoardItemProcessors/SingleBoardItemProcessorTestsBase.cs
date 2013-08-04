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

        protected void MockEventTimeFrameCreator(DateTime itemDue, User user, TimeStamp eventStart = null, TimeStamp eventEnd = null)
        {
            var timeZone = user != null ? user.CalendarTimeZone : null;

            var wholeDayIndicator = user != null && user.UserPreferences != null
                                        ? user.UserPreferences.WholeDayEventDueTime
                                        : null;

            AutoMoqer.GetMock<IEventTimeFrameCreator>()
                     .Setup(x => x.CreateFromUTC(itemDue, timeZone, wholeDayIndicator))
                     .Returns(new Tuple<TimeStamp, TimeStamp>(eventStart, eventEnd));
        }

        protected Event TestProcess(TItem item, string parentName)
        {
            // Act
            return AutoMoqer.Resolve<TProcessor>().Process(item, parentName);
        }

        protected Event TestProcess(TItem item, string parentName, User user)
        {
            // Arrange
            AutoMoqer.SetInstance(new UserContext { User = user });

            // Act
            return AutoMoqer.Resolve<TProcessor>().Process(item, parentName);
        }
    }
}
