using Ninject.Modules;
using Trellendar.DataAccess;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.DataAccess._Impl;

namespace Trellendar.Service.Bootstrap.NinjectModules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            // Data Access
            Bind<IAccessTokenProviderFactory>().To<AccessTokenProviderFactory>();
            Bind<ICalendarAccessTokenExpirationHandler>().To<CalendarAccessTokenExpirationHandler>();
        }
    }
}
