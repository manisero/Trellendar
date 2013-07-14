using Ninject.Modules;
using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Trello;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.DataAccess._Impl;

namespace Trellendar.Service.Bootstrap.NinjectModules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            // Data Access
            Bind<ICalendarAccessTokenExpirationHandler>().To<CalendarAccessTokenExpirationHandler>();
            Bind<ITrelloAccessTokenProvider>().To<AccessTokensProvider>();
            Bind<ICalendarAccessTokenProvider>().To<AccessTokensProvider>();
        }
    }
}
