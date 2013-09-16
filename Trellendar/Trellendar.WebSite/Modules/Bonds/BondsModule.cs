using System.Collections.Generic;
using Nancy;
using Nancy.Security;
using Trellendar.Core.Extensions.AutoMapper;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic;
using Trellendar.Logic.UserManagement;
using System.Linq;
using Nancy.ModelBinding;
using Trellendar.WebSite.Modules.Bonds.Models;
using Trellendar.WebSite.Modules._Shared;

namespace Trellendar.WebSite.Modules.Bonds
{
    public class BondsModule : NancyModule
    {
        private readonly UserContext _userContext;
        private readonly IUserService _userService;

        public BondsModule(UserContext userContext, IUserService userService)
            : base("Bonds")
        {
            _userContext = userContext;
            _userService = userService;

            this.RequiresAuthentication();

            Get["/"] = Index;
            Post["/Save"] = Save;
        }

        public dynamic Index(dynamic parameters)
        {
            var boards = _userService.GetAvailableBoards().OrderBy(x => x.Name);
            var calendars = _userService.GetAvailableCalendars().OrderBy(x => x.Summary);

            var model = new IndexModel
                {
                    Email = _userContext.User.Email,
                    BoardCalendarBonds = _userContext.User.BoardCalendarBonds.MapTo<BoardCalendarBondModel>(),
                    AvailableBoards = boards.MapTo<BoardModel>(),
                    AvailableCalendars = calendars.MapTo<CalendarModel>()
                };

            return View[model];
        }

        public dynamic Save(dynamic parameters)
        {
            var model = this.Bind<IEnumerable<BoardCalendarBondModel>>();

            if (model == null)
            {
                return new AjaxResponse
                    {
                        Success = false,
                        ErrorMessage = "Invalid request"
                    };
            }

            _userService.UpdateBoardCalendarBonds(model.Select(x => new BoardCalendarBond
                {
                    BoardID = x.BoardID,
                    CalendarID = x.CalendarID
                }));

            return new AjaxResponse { Success = true };
        }
    }
}