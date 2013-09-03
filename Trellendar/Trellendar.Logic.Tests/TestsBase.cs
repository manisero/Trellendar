using NUnit.Framework;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.Tests
{
    [TestFixture]
    public abstract class TestsBase : TestsCore.TestsBase
    {
        protected void MockUserContext(User user)
        {
            AutoMoqer.SetInstance(new UserContext(user));
        }

        protected void MockBoardCalendarContext(BoardCalendarBond boardCalendarBond)
        {
            AutoMoqer.SetInstance(new BoardCalendarContext(boardCalendarBond));
        }

        protected void MockBoardCalendarContext(BoardCalendarBondSettings boardCalendarBondSettings)
        {
            AutoMoqer.SetInstance(new BoardCalendarContext(new BoardCalendarBond { Settings = boardCalendarBondSettings }));
        }
    }
}
