using System;
using System.Collections.Generic;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using System.Linq;

namespace Trellendar.Logic.UserManagement._Impl
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICalendarAPI _calendarApi;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserContext userContext, ITrelloAPI trelloApi, ICalendarAPI calendarApi, IUnitOfWork unitOfWork)
        {
            _userContext = userContext;
            _trelloApi = trelloApi;
            _calendarApi = calendarApi;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Board> GetAvailableBoards()
        {
            return _trelloApi.GetBoards();
        }

        public IEnumerable<Calendar> GetAvailableCalendars()
        {
            return _calendarApi.GetCalendars();
        }

        public void UpdateBoardCalendarBonds(IEnumerable<BoardCalendarBond> bonds)
        {
            var existingBonds = _userContext.User.BoardCalendarBonds;

            var toRemove = existingBonds.Where(x => !bonds.Any(bond => bond.BoardID == x.BoardID && bond.CalendarID == x.CalendarID)).ToList();

            toRemove.ForEach(x => existingBonds.Remove(x));

            var toAdd = bonds.Where(x => !existingBonds.Any(bond => bond.BoardID == x.BoardID && bond.CalendarID == x.CalendarID)).ToList();

            foreach (var bond in toAdd)
            {
                bond.LastSynchronizationTS = new DateTime(1900, 1, 1);
                bond.CreateTS = DateTime.UtcNow;
                existingBonds.Add(bond);
            }

            _unitOfWork.SaveChanges();
        }
    }
}