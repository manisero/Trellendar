using Ninject.Modules;
using Trellendar.DataAccess.Remote;
using Trellendar.Logic;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.CalendarSynchronization._Impl;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.DataAccess._Impl;
using Trellendar.Logic.TimeZones;
using Trellendar.Logic.TimeZones._Impl;
using Trellendar.Logic.UserProfileSynchronization;
using Trellendar.Logic.UserProfileSynchronization._Impl;
using Trellendar.Logic._Impl;

namespace Trellendar.Service.Ninject.Modules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISynchronizationService>().To<SynchronizationService>();

            // Calendar Synchronization
            Bind<ICalendarService>().To<CalendarService>();
            Bind<IBoardItemsProcessor>().To<BoardItemsProcessor>();
            Bind<ISingleBoardItemProcessorFactory>().To<SingleBoardItemProcessorFactory>();
            Bind<IEventTimeFrameCreator>().To<EventTimeFrameCreator>();

            // User Profile Synchronization
            Bind<IUserProfileService>().To<UserProfileService>();

            // Data Access
            Bind<IAccessTokenProviderFactory>().To<AccessTokenProviderFactory>();
            Bind<ICalendarAccessTokenExpirationHandler>().To<CalendarAccessTokenExpirationHandler>();

            // Time Zones
            Bind<ITimeZoneService>().To<TimeZoneService>();
        }
    }
}
