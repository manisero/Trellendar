using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.UserManagement
{
    public interface IUserService
    {
        IEnumerable<Board> GetAvailableBoards();

        IEnumerable<Calendar> GetAvailableCalendars();

        void UpdateBoardCalendarBonds(IEnumerable<BoardCalendarBond> bonds);
    }
}
