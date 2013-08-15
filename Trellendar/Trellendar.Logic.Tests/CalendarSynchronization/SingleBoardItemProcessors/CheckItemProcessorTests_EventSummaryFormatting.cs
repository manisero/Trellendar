using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Tests.CalendarSynchronization.SingleBoardItemProcessors
{
    public partial class CheckItemProcessorTests
    {
        [Test]
		[Sequential]
		public void formats_not_done_event_summary___without_event_name_template(
			[Values("name", "important thing")] string checkItemName,
			[Values("name", "important thing")] string expectedSummary)
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
								.With(x => x.Name = checkItemName)
								.With(x => x.State = CheckItemExtensions.STATE_NOT_DONE)
								.Build();

			// Arrange, Act & Assert
			TestFormatEventSummary(checkItem, null, "not important", expectedSummary);
        }

        [Test]
		[Sequential]
		public void formats_done_event_summary___without_event_name_template(
			[Values("name", "important thing")] string checkItemName,
			[Values("suffix", " (done)")] string doneSuffix,
			[Values("namesuffix", "important thing (done)")] string expectedSummary)
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
							.With(x => x.Name = checkItemName)
							.With(x => x.State = CheckItemExtensions.STATE_DONE)
							.Build();

	        var preferences = Builder<UserPreferences>.CreateNew()
								.With(x => x.CheckListEventDoneSuffix = doneSuffix)
								.With(x => x.CheckListEventNameTemplate = null)
								.Build();

			// Arrange, Act & Assert
            TestFormatEventSummary(checkItem, preferences, "not important", expectedSummary);
        }

		[Test]
		[Sequential]
		public void formats_not_done_event_summary___with_event_name_template___without_checklist_shortcut_markers(
			[Values("checkList", "things")] string checkListName,
			[Values("name", "important thing")] string checkItemName,
			[Values("{0}{1}", "[{0}] {1}")] string eventNameTemplate,
			[Values("checkListname", "[things] important thing")] string expectedSummary)
		{
			// Arrange
			var checkItem = Builder<CheckItem>.CreateNew()
								.With(x => x.Name = checkItemName)
								.With(x => x.State = CheckItemExtensions.STATE_NOT_DONE)
								.Build();

			var preferences = Builder<UserPreferences>.CreateNew()
								.With(x => x.CheckListEventNameTemplate = eventNameTemplate)
								.With(x => x.CheckListShortcutBeginningMarker = null)
								.With(x => x.CheckListShortcutEndMarker = null)
								.Build();

			// Arrange, Act & Assert
			TestFormatEventSummary(checkItem, preferences, checkListName, expectedSummary);
		}

		[Test]
		[Sequential]
		public void formats_done_event_summary___with_event_name_template___without_checklist_shortcut_markers(
			[Values("checkList", "things")] string checkListName,
			[Values("name", "important thing")] string checkItemName,
			[Values("{0}{1}", "[{0}] {1}")] string eventNameTemplate,
			[Values("suffix", " (done)")] string doneSuffix,
			[Values("checkListnamesuffix", "[things] important thing (done)")] string expectedSummary)
		{
			// Arrange
			var checkItem = Builder<CheckItem>.CreateNew()
								.With(x => x.Name = checkItemName)
								.With(x => x.State = CheckItemExtensions.STATE_DONE)
								.Build();

			var preferences = Builder<UserPreferences>.CreateNew()
								.With(x => x.CheckListEventNameTemplate = eventNameTemplate)
								.With(x => x.CheckListEventDoneSuffix = doneSuffix)
								.With(x => x.CheckListShortcutBeginningMarker = null)
								.With(x => x.CheckListShortcutEndMarker = null)
								.Build();

			// Arrange, Act & Assert
			TestFormatEventSummary(checkItem, preferences, checkListName, expectedSummary);
		}

		[Test]
		[Sequential]
		public void formats_not_done_event_summary___with_event_name_template___with_checklist_shortcut_markers(
			[Values("checkList [ch]", "things <short>th</short>", "checkList")] string checkListName,
			[Values("[", "<short>", "not_present")] string shortcutBeginningMarker,
			[Values("]", "</short>", "not present")] string shortcutEndMarker,
			[Values("name", "important thing", "name")] string checkItemName,
			[Values("{0}{1}", "[{0}] {1}", "{0}{1}")] string eventNameTemplate,
			[Values("chname", "[th] important thing", "checkListname")] string expectedSummary)
		{
			// Arrange
			var checkItem = Builder<CheckItem>.CreateNew()
								.With(x => x.Name = checkItemName)
								.With(x => x.State = CheckItemExtensions.STATE_NOT_DONE)
								.Build();

			var preferences = Builder<UserPreferences>.CreateNew()
								.With(x => x.CheckListEventNameTemplate = eventNameTemplate)
								.With(x => x.CheckListShortcutBeginningMarker = shortcutBeginningMarker)
								.With(x => x.CheckListShortcutEndMarker = shortcutEndMarker)
								.Build();

			// Arrange, Act & Assert
			TestFormatEventSummary(checkItem, preferences, checkListName, expectedSummary);
		}

		[Test]
		[Sequential]
		public void formats_done_event_summary___with_event_name_template___with_checklist_shortcut_markers(
			[Values("checkList [ch]", "things <short>th</short>", "checkList")] string checkListName,
			[Values("[", "<short>", "not_present")] string shortcutBeginningMarker,
			[Values("]", "</short>", "not present")] string shortcutEndMarker,
			[Values("name", "important thing", "name")] string checkItemName,
			[Values("{0}{1}", "[{0}] {1}", "{0}{1}")] string eventNameTemplate,
			[Values("suffix", " (done)", "suffix")] string doneSuffix,
			[Values("chnamesuffix", "[th] important thing (done)", "checkListnamesuffix")] string expectedSummary)
		{
			// Arrange
			var checkItem = Builder<CheckItem>.CreateNew()
								.With(x => x.Name = checkItemName)
								.With(x => x.State = CheckItemExtensions.STATE_DONE)
								.Build();

			var preferences = Builder<UserPreferences>.CreateNew()
								.With(x => x.CheckListEventNameTemplate = eventNameTemplate)
								.With(x => x.CheckListShortcutBeginningMarker = shortcutBeginningMarker)
								.With(x => x.CheckListShortcutEndMarker = shortcutEndMarker)
								.With(x => x.CheckListEventDoneSuffix = doneSuffix)
								.Build();

			// Arrange, Act & Assert
			TestFormatEventSummary(checkItem, preferences, checkListName, expectedSummary);
		}

		private void TestFormatEventSummary(CheckItem checkItem, UserPreferences preferences, string checkListName, string expectedSummary)
		{
			var due = Builder<Due>.CreateNew().With(x => x.HasTime = false).Build();

			MockUserContext(preferences);

			AutoMoqer.GetMock<IParser<Due>>().Setup(x => x.Parse(checkItem.Name, preferences)).Returns(due);
			MockTimeFrameCreation_WholeDay(due.DueDateTime);

			// Act
			var result = TestProcess(checkItem, checkListName);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedSummary, result.Summary);	
		}
    }
}
