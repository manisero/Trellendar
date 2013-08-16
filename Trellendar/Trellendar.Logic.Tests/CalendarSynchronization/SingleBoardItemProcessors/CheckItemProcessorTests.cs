using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.CalendarSynchronization.Formatters;
using Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    [TestFixture]
    public partial class CheckItemProcessorTests : SingleBoardItemProcessorTestsBase<CheckItemProcessor, CheckItem>
    {
        [Test]
        public void sets_event_extended_properties_properly()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();

            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, null)).Returns(due);
            MockTimeFrameCreation_WholeDay(due.DueDateTime);

            // Act
            var result = TestProcess(checkItem, "not important", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ExtendedProperties);

            var properties = result.ExtendedProperties.Private;
            Assert.IsNotNull(properties);
            Assert.IsTrue(properties.ContainsKey(EventExtensions.GENERATED_PROPERTY_KEY));
            Assert.IsTrue(properties.ContainsKey(EventExtensions.SOURCE_ID_PROPERTY_KEY));
            Assert.AreEqual(checkItem.Id, properties[EventExtensions.SOURCE_ID_PROPERTY_KEY]);
        }

        [Test]
        public void sets_event_description_properly()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var descritpion = "descritpion";

            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();
            
            AutoMoqer.GetMock<ICheckItemDescriptionFormatter>().Setup(x => x.Format(checkItem, preferences)).Returns(descritpion);

            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, preferences)).Returns(due);
            MockTimeFrameCreation_WholeDay(due.DueDateTime);

            // Act
            var result = TestProcess(checkItem, "not important", new User { UserPreferences = preferences });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(descritpion, result.Description);
        }

        protected override object GetExptectedItemKey(CheckItem item)
        {
            return item.Id;
        }
    }
}
