using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Logic.Synchronization.CalendarSynchronization;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters;

namespace Trellendar.Logic.Tests.Synchronization.CalendarSynchronization.Formatting
{
    public class CheckItemLocationFormatterTests : TestsBase
    {
        [Test]
        public void returns_parsed_location()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var settings = Builder<BoardCalendarBondSettings>.CreateNew().Build();
            var location = Builder<Location>.CreateNew().Build();

            AutoMoqer.GetMock<IParser<Location>>().Setup(x => x.Parse(checkItem.Name, settings)).Returns(location);

            // Acr
            var result = AutoMoqer.Resolve<CheckItemLocationFormatter>().Format(checkItem, settings);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(location.Value, result);
        }

        [Test]
        public void returns_null_for_null_location()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var settings = Builder<BoardCalendarBondSettings>.CreateNew().Build();

            AutoMoqer.GetMock<IParser<Location>>().Setup(x => x.Parse(checkItem.Name, settings)).Returns((Location)null);

            // Acr
            var result = AutoMoqer.Resolve<CheckItemLocationFormatter>().Format(checkItem, settings);

            // Assert
            Assert.IsNull(result);
        }
    }
}
