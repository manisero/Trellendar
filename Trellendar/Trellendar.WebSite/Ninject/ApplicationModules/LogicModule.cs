using Ninject.Modules;
using Trellendar.DataAccess.Remote;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.DataAccess._Impl;
using Trellendar.Logic.UserManagement;
using Trellendar.Logic.UserManagement._Impl;

namespace Trellendar.WebSite.Ninject.ApplicationModules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            // User Management
            Bind<IUserService>().To<UserService>();

            // Data Access
            Bind<IAccessTokenProviderFactory>().To<AccessTokenProviderFactory>();
            Bind<ICalendarAccessTokenExpirationHandler>().To<CalendarAccessTokenExpirationHandler>();
        }
    }
}
