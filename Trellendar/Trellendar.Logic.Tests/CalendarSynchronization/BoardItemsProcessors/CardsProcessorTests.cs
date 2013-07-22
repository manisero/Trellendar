using AutoMoq;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.BoardItemsProcessors;

namespace Trellendar.Logic.Tests.CalendarSynchronization.BoardItemsProcessors
{
    [TestFixture]
    public class CardsProcessorTests
    {
        [Test]
        public void returns_null_for_closed_card()
        {
            var autoMoqer = new AutoMoqer();

            // Arrange
            var card = Builder<Card>.CreateNew().Build();

            // Act
            autoMoqer.Resolve<CardsProcessor>().Process(new[] { card }, "not important", new Event[0]);

            // Assert
        }
    }
}
