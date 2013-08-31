using System.Data.Entity;
using Ninject.Modules;
using Trellendar.DataAccess.Local;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.DataAccess.Local.Repository._Impl;
using Trellendar.DataAccess.Remote;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Calendar._Impl;
using Trellendar.DataAccess.Remote.Google;
using Trellendar.DataAccess.Remote.Google._Impl;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.DataAccess.Remote.Trello._Impl;
using Trellendar.DataAccess.Remote._Impl;

namespace Trellendar.Service.Ninject.Modules
{
    internal class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            // Local
            Bind<DbContext>().ToConstant(new TrellendarDataContext());
            Bind<IUnitOfWork>().To<EntityFrameworkUnitOfWork>();
            Bind<IRepositoryFactory>().To<EntityFrameworkRepositoryFactory>();

            // Remote
            Bind<IRestClientFactory>().To<RestClientFactory>();

            // Trello
            Bind<ITrelloAuthorizationAPI>().To<TrelloAuthorizationAPI>();
            Bind<ITrelloAPI>().To<TrelloAPI>();

            // Calendar
            Bind<IGoogleAuthorizationAPI>().To<GoogleAuthorizationAPI>();
            Bind<ICalendarAPI>().To<CalendarAPI>();
        }
    }
}
