using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters;

namespace Trellendar.Logic.Tests.Synchronization.CalendarSynchronization.Formatting
{
    [TestFixture]
    public class CardDescriptionFormatterTests : TestsBase
    {
        [Test]
        public void appends_card_url(
            [Values("description")] string cardDescription,
            [Values("url")] string cardUrl,
            [Values("description\n\nLink: url")] string expectedDescription)
        {
            // Arrange
            var card = Builder<Card>.CreateNew()
                                    .With(x => x.Description = cardDescription)
                                    .With(x => x.Url = cardUrl)
                                    .Build();

            // Act
            var result = AutoMoqer.Resolve<CardDescriptionFormatter>().Format(card, null);

            // Assert
            Assert.AreEqual(expectedDescription, result);
        }
    }
}
