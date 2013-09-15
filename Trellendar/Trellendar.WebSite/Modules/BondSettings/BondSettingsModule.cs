using AutoMapper;
using Nancy;
using Nancy.Security;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic;
using Trellendar.Logic.UserManagement;
using Trellendar.WebSite.Modules.BondSettings.Models;
using Nancy.ModelBinding;
using Trellendar.Core.Extensions;

namespace Trellendar.WebSite.Modules.BondSettings
{
    public class BondSettingsModule : NancyModule
    {
        private readonly UserContext _userContext;
        private readonly IUserService _userService;

        public BondSettingsModule(UserContext userContext, IUserService userService)
            : base("BondSettings")
        {
            _userContext = userContext;
            _userService = userService;
            this.RequiresAuthentication();

            Get["/"] = Index;
            Post["/Save"] = Save;
        }

        public dynamic Index(dynamic parameters)
        {
            var model = Mapper.Map<BoardCalendarBondSettings, IndexModel>(_userContext.User.DefaultBondSettings);

            return View[model];
        }

        public dynamic Save(dynamic parameters)
        {
            var model = this.BindAndValidate<IndexModel>();

            _userService.UpdateDefaultBoardCalendarBondSettings(model.MapTo<BoardCalendarBondSettings>());

            return null;
        }
    }
}