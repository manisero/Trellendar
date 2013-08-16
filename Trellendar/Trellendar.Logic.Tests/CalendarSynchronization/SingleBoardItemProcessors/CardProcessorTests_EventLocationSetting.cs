using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    public partial class CardProcessorTests
    {
        [Test]
        public void sets_event_location___not_null_location()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var user = Builder<User>.CreateNew().With(x => x.UserPreferences = new UserPreferences()).Build();
            var expectedLocation = "location";

            AutoMoqer.GetMock<IParser<Location>>()
                     .Setup(x => x.Parse(card.Description, user.UserPreferences))
                     .Returns(new Location { Value = expectedLocation });

            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(card, user);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(expectedLocation, result.Location);
        }

        [Test]
        public void sets_event_location___null_location()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();
            var user = Builder<User>.CreateNew().With(x => x.UserPreferences = new UserPreferences()).Build();

            AutoMoqer.GetMock<IParser<Location>>()
                     .Setup(x => x.Parse(card.Description, user.UserPreferences))
                     .Returns((Location)null);

            MockTimeFrameFormatting();

            // Act
            var result = TestProcess(card, user);

            // Assert
            Assert.NotNull(result);
            Assert.IsNull(result.Location);
        }
    }
}
