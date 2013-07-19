using Ninject.Modules;
using Trellendar.DataAccess;
using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Calendar._Impl;
using Trellendar.DataAccess.Native;
using Trellendar.DataAccess.Trello;
using Trellendar.DataAccess.Trello._Impl;
using Trellendar.DataAccess._Impl;

namespace Trellendar.Service.Ninject.Modules
{
    internal class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRestClientFactory>().To<RestClientFactory>();

            // Native
            Bind<TrellendarDataContext>().ToConstant(new TrellendarDataContext());

            // Trello
            Bind<ITrelloAuthorizationAPI>().To<TrelloAuthorizationAPI>();
            Bind<ITrelloAPI>().To<TrelloAPI>();

            // Calendar
            Bind<ICalendarAuthorizationAPI>().To<CalendarAuthorizationAPI>();
            Bind<ICalendarAPI>().To<CalendarAPI>();
        }
    }
}
