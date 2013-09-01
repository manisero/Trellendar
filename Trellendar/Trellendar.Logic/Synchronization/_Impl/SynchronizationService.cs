using Trellendar.Core.Extensions;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Domain.Trellendar;
using System.Linq;
using Trellendar.Logic.Domain;
using Trellendar.Logic.Synchronization.BoardCalendarBondSynchronization;
using Trellendar.Logic.Synchronization.CalendarSynchronization;

namespace Trellendar.Logic.Synchronization._Impl
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly BoardCalendarContext _boardCalendarContext;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICalendarAPI _calendarApi;
        private readonly IBoardCalendarBondSynchronizationService _boardCalendarSynchronizationService;
        private readonly ICalendarSynchronizationService _calendarSynchronizationService;

        private BoardCalendarBond BoardCalendarBond
        {
            get { return _boardCalendarContext.BoardCalendarBond; }
        }

        public SynchronizationService(BoardCalendarContext boardCalendarContext, ITrelloAPI trelloApi, ICalendarAPI calendarApi,
                                      IBoardCalendarBondSynchronizationService boardCalendarBondSynchronizationService, 
                                      ICalendarSynchronizationService calendarSynchronizationService)
        {
            _boardCalendarContext = boardCalendarContext;
            _trelloApi = trelloApi;
            _calendarApi = calendarApi;
            _boardCalendarSynchronizationService = boardCalendarBondSynchronizationService;
            _calendarSynchronizationService = calendarSynchronizationService;
        }

        public void Synchronize()
        {
            var board = _trelloApi.GetBoard(BoardCalendarBond.BoardID);

            if (board.Lists.IsNullOrEmpty() || board.Cards.IsNullOrEmpty())
            {
                return;
            }

            var calendarEvents = _calendarApi.GetEvents(BoardCalendarBond.CalendarID);

            _boardCalendarSynchronizationService.UpdateBond(calendarEvents);
            _boardCalendarSynchronizationService.UpdateBondSettings(board);
            _calendarSynchronizationService.UpdateCalendar(board, calendarEvents.Items.Where(x => x.IsGeneratedByTrellendar()));
        }
    }
}