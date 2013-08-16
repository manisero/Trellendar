using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Calendar;
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
        public void sets_event_summary_properly()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var preferences = Builder<UserPreferences>.CreateNew().Build();
            var summary = "summary";

            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();

            AutoMoqer.GetMock<ICheckItemSummaryFormatter>().Setup(x => x.Format(checkItem, preferences)).Returns(summary);

            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, preferences)).Returns(due);
            MockTimeFrameCreation_WholeDay(due.DueDateTime);

            // Act
            var result = TestProcess(checkItem, "not important", new User { UserPreferences = preferences });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(summary, result.Summary);
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

        [Test]
        public void sets_event_extended_properties_properly()
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew().Build();
            var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();
            var extendedProperties = Builder<EventExtendedProperties>.CreateNew().Build();

            AutoMoqer.GetMock<ICheckItemExtendedPropertiesFormatter>().Setup(x => x.Format(checkItem)).Returns(extendedProperties);

            AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, null)).Returns(due);
            MockTimeFrameCreation_WholeDay(due.DueDateTime);

            // Act
            var result = TestProcess(checkItem, "not important", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(extendedProperties, result.ExtendedProperties);
        }

        protected override object GetExptectedItemKey(CheckItem item)
        {
            return item.Id;
        }
    }
}
