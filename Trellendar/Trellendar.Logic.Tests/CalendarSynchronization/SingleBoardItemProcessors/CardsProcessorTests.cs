using AutoMoq;
using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
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
            autoMoqer.Resolve<CardProcessor>().Process(card, "not important");

            // Assert
        }
    }
}
