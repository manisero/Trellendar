using FizzWare.NBuilder;
using NUnit.Framework;
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
    }
}
