using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    public partial class CardProcessorTests
    {
        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_user()
        {
            // Arrange
            var card = Builder<Card>.CreateNew().Build();

            MockEventTimeFrameCreator(card.Due.Value, null);

            // Act
            var result = TestProcess(card, "not important", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(card.Name, result.summary);
        }

        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_preferences()
        {
            // Arrange
            var user = Builder<User>.CreateNew().With(x => x.UserPreferences = null).Build();
            var card = Builder<Card>.CreateNew().Build();

            MockEventTimeFrameCreator(card.Due.Value, user);

            // Act
            var result = TestProcess(card, "not important", user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(card.Name, result.summary);
        }

        [Test]
        public void formats_summary_for_null_preffered_event_name_template___null_template()
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew()
                                        .With(preferences => preferences.CardEventNameTemplate = null)
                                        .Build())
                                    .Build();

            var card = Builder<Card>.CreateNew().Build();

            MockEventTimeFrameCreator(card.Due.Value, user);

            // Act
            var result = TestProcess(card, "not important", user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(card.Name, result.summary);
        }

        [Test]
        public void formats_summary_for_not_null_preffered_event_name_template___null_list_shortcut_markers(
            [Values("{0}{1}", "[{0}] {1}", "{1} ({0})", "{1}")] string eventNameTemplate)
        {
            // Arrange
            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew()
                                        .With(preferences => preferences.CardEventNameTemplate = eventNameTemplate)
                                        .Build())
                                    .Build();

            var listName = "list";
            var card = Builder<Card>.CreateNew().Build();

            var expectedSummary = string.Format(eventNameTemplate, listName, card.Name);

            MockEventTimeFrameCreator(card.Due.Value, user);

            // Act
            var result = TestProcess(card, listName, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSummary, result.summary);
        }

        [Test]
        public void formats_summary_for_not_null_preffered_event_name_template___not_matching_list_shortcut_markers(
            [Values("list name")] string lisName,
            [Values((string)null, "(", "m")] string beginningMarker,
            [Values((string)null, ")", "l")] string endMarker)
        {
            // Arrange
            var eventNameTemplate = "[{0}] {1}";

            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew()
                                        .With(preferences => preferences.ListShortcutBeginningMarker = beginningMarker)
                                        .With(preferences => preferences.ListShortcutEndMarker = endMarker)
                                        .With(preferences => preferences.CardEventNameTemplate = eventNameTemplate)
                                        .Build())
                                    .Build();

            var listName = lisName;
            var card = Builder<Card>.CreateNew().Build();

            var expectedSummary = string.Format(eventNameTemplate, listName, card.Name);

            MockEventTimeFrameCreator(card.Due.Value, user);

            // Act
            var result = TestProcess(card, listName, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSummary, result.summary);
        }

        [Test]
        [Sequential]
        public void formats_summary_for_not_null_preffered_event_name_template___matching_list_shortcut_markers(
            [Values("(", "[", "{", "([", "([{")] string beginningMarker,
            [Values(")", "]", "}", "])", "}])")] string endMarker,
            [Values("[{cut}] card", "{cut} card", "cut card", "{cut} card", "cut card")] string expectedSummary)
        {
            // Arrange
            var listName = "list name ([{cut}])";
            var cardName = "card";
            var eventNameTemplate = "{0} {1}";

            var user = Builder<User>.CreateNew()
                                    .With(x => x.UserPreferences = Builder<UserPreferences>.CreateNew()
                                        .With(preferences => preferences.ListShortcutBeginningMarker = beginningMarker)
                                        .With(preferences => preferences.ListShortcutEndMarker = endMarker)
                                        .With(preferences => preferences.CardEventNameTemplate = eventNameTemplate)
                                        .Build())
                                    .Build();

            var card = Builder<Card>.CreateNew().With(x => x.Name = cardName).Build();

            MockEventTimeFrameCreator(card.Due.Value, user);

            // Act
            var result = TestProcess(card, listName, user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSummary, result.summary);
        }
    }
}
