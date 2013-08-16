using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatters._Impl;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatters
{
    [TestFixture]
    public class CardExtendedPropertiesFormatterTests : TestsBase
    {
        [Test]
        public void formats_extended_properties()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();

            // Act
            var result = AutoMoqer.Resolve<CardExtendedPropertiesFormatter>().Format(card);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Private);
            Assert.IsTrue(result.Private.ContainsKey(EventExtensions.GENERATED_PROPERTY_KEY));
            Assert.IsTrue(result.Private.ContainsKey(EventExtensions.SOURCE_ID_PROPERTY_KEY));
            Assert.AreEqual(card.Id, result.Private[EventExtensions.SOURCE_ID_PROPERTY_KEY]);
        }
    }
}
