using FizzWare.NBuilder;
using NUnit.Framework;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Logic.Synchronization.CalendarSynchronization;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters;

namespace Trellendar.Logic.Tests.CalendarSynchronization.Formatting
{
    [TestFixture]
    public class CheckItemSummaryFormatterTests : TestsBase
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

            var setting = Builder<BoardCalendarBondSettings>.CreateNew()
                                                            .With(x => x.CheckListEventDoneSuffix = doneSuffix)
                                                            .With(x => x.CheckListEventNameTemplate = null)
                                                            .Build();

            // Arrange, Act & Assert
            TestFormatEventSummary(checkItem, setting, "not important", expectedSummary);
        }

        [Test]
        [Sequential]
        public void formats_not_done_event_summary___with_event_name_template(
            [Values("checkList [ch]", "things <short>th</short>", "checkList")] string checkListName,
            [Values("ch", "th", "checkList")] string checkListShortcut,
            [Values("name", "important thing", "name")] string checkItemName,
            [Values("{0}{1}", "[{0}] {1}", "{0}{1}")] string eventNameTemplate,
            [Values("chname", "[th] important thing", "checkListname")] string expectedSummary)
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                                .With(x => x.Name = checkItemName)
                                .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                    .With(checkList => checkList.Name = checkListName)
                                    .Build())
                                .With(x => x.State = CheckItemExtensions.STATE_NOT_DONE)
                                .Build();

            var setting = Builder<BoardCalendarBondSettings>.CreateNew()
                                                            .With(x => x.CheckListEventNameTemplate = eventNameTemplate)
                                                            .Build();

            // Arrange, Act & Assert
            TestFormatEventSummary(checkItem, setting, checkListShortcut, expectedSummary);
        }

        [Test]
        [Sequential]
        public void formats_done_event_summary___with_event_name_template(
            [Values("checkList [ch]", "things <short>th</short>", "checkList")] string checkListName,
            [Values("ch", "th", "checkList")] string checkListShortcut,
            [Values("name", "important thing", "name")] string checkItemName,
            [Values("{0}{1}", "[{0}] {1}", "{0}{1}")] string eventNameTemplate,
            [Values("suffix", " (done)", "suffix")] string doneSuffix,
            [Values("chnamesuffix", "[th] important thing (done)", "checkListnamesuffix")] string expectedSummary)
        {
            // Arrange
            var checkItem = Builder<CheckItem>.CreateNew()
                                .With(x => x.Name = checkItemName)
                                .With(x => x.CheckList = Builder<CheckList>.CreateNew()
                                    .With(checkList => checkList.Name = checkListName)
                                    .Build())
                                .With(x => x.State = CheckItemExtensions.STATE_DONE)
                                .Build();

            var setting = Builder<BoardCalendarBondSettings>.CreateNew()
                                                            .With(x => x.CheckListEventNameTemplate = eventNameTemplate)
                                                            .With(x => x.CheckListEventDoneSuffix = doneSuffix)
                                                            .Build();

            // Arrange, Act & Assert
            TestFormatEventSummary(checkItem, setting, checkListShortcut, expectedSummary);
        }

        private void TestFormatEventSummary(CheckItem checkItem, BoardCalendarBondSettings settings, string checkListShortcut, string expectedSummary)
        {
            // Arrange
            if (checkItem.CheckList != null)
            {
                AutoMoqer.GetMock<IParser<BoardItemName>>()
                         .Setup(x => x.Parse(checkItem.CheckList.Name, settings))
                         .Returns(new BoardItemName { Value = checkListShortcut });
            }

            // Act
            var result = AutoMoqer.Resolve<CheckItemSummaryFormatter>().Format(checkItem, settings);

            // Assert
            Assert.AreEqual(expectedSummary, result);
        }
    }
}
