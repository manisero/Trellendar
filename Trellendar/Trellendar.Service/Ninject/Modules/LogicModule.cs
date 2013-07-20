using Ninject.Modules;
using Trellendar.DataAccess.Remote;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.CalendarSynchronization._Impl;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.DataAccess._Impl;

namespace Trellendar.Service.Ninject.Modules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            // Calendar Synchronization
            Bind<ISynchronizationService>().To<SynchronizationService>();
            Bind<IUserProfileService>().To<UserProfileService>();
            Bind<ICardProcessor>().To<CardProcessor>();

            // Data Access
            Bind<IAccessTokenProviderFactory>().To<AccessTokenProviderFactory>();
            Bind<ICalendarAccessTokenExpirationHandler>().To<CalendarAccessTokenExpirationHandler>();
        }
    }
}
