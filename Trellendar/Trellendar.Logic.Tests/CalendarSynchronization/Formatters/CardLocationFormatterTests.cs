using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.CalendarSynchronization.Formatters._Impl;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatters
{
    [TestFixture]
    public class CardLocationFormatterTests : TestsBase
    {
        [Test]
        public void returns_parsed_location()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var userPreferences = Builder<UserPreferences>.CreateNew().Build();
            var location = Builder<Location>.CreateNew().Build();

            AutoMoqer.GetMock<IParser<Location>>().Setup(x => x.Parse(card.Description, userPreferences)).Returns(location);

            // Acr
            var result = AutoMoqer.Resolve<CardLocationFormatter>().Format(card, userPreferences);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(location.Value, result);
        }

        [Test]
        public void returns_null_for_null_location()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var userPreferences = Builder<UserPreferences>.CreateNew().Build();

            AutoMoqer.GetMock<IParser<Location>>().Setup(x => x.Parse(card.Description, userPreferences)).Returns((Location)null);

            // Acr
            var result = AutoMoqer.Resolve<CardLocationFormatter>().Format(card, userPreferences);

            // Assert
            Assert.IsNull(result);
        }
    }
}
