using System.Collections.Generic;
using Nancy;
using Nancy.Security;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic;
using Trellendar.Logic.UserManagement;
using Trellendar.WebSite.Modules.UserProfile.Models;
using System.Linq;
using Nancy.ModelBinding;
using Trellendar.WebSite.Modules._Shared;

namespace Trellendar.WebSite.Modules.UserProfile
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
                    BoardCalendarBonds = _userContext.User.BoardCalendarBonds.Select(x => new BoardCalendarBondModel
                        {
                            BoardID = x.BoardID,
                            CalendarID = x.CalendarID
                        }),
                    AvailableBoards = boards.Select(x => new BoardModel
                        {
                            ID = x.Id,
                            Name = x.Name
                        }),
                    AvailableCalendars = calendars.Select(x => new CalendarModel
                        {
                            ID = x.Id,
                            Name = x.Summary
                        })
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