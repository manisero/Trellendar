using Ninject.Modules;
using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Calendar._Impl;
using Trellendar.DataAccess.Trello;
using Trellendar.DataAccess.Trello._Impl;

namespace Trellendar.Service.Bootstrap.NinjectModules
{
    internal class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            // Trello
            Bind<ITrelloClient>().To<TrelloClient>();
            Bind<IAuthorizedTrelloClient>().To<AuthorizedTrelloClient>();
            Bind<ITrelloAPI>().To<TrelloAPI>();

            // Calendar
            Bind<ICalendarClient>().To<CalendarClient>();
            Bind<IAuthorizedCalendarClient>().To<AuthorizedCalendarClient>();
            Bind<ICalendarAuthorizationAPI>().To<CalendarAuthorizationAPI>();
            Bind<ICalendarAPI>().To<CalendarAPI>();
        }
    }
}
