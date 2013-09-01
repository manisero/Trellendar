using Ninject.Modules;
using Trellendar.DataAccess.Remote;
using Trellendar.Domain.Trello;
using Trellendar.Logic;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.DataAccess._Impl;
using Trellendar.Logic.Domain;
using Trellendar.Logic.Synchronization;
using Trellendar.Logic.Synchronization.BoardCalendarBondSynchronization;
using Trellendar.Logic.Synchronization.BoardCalendarBondSynchronization._Impl;
using Trellendar.Logic.Synchronization.CalendarSynchronization;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters;
using Trellendar.Logic.Synchronization.CalendarSynchronization.Parsers;
using Trellendar.Logic.Synchronization.CalendarSynchronization._Impl;
using Trellendar.Logic.Synchronization._Impl;
using Trellendar.Logic.TimeZones;
using Trellendar.Logic.TimeZones._Impl;
using Trellendar.Logic.UserManagement;
using Trellendar.Logic.UserManagement._Impl;

namespace Trellendar.Service.Ninject.Modules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISynchronizationService>().To<SynchronizationService>();

            // Calendar Synchronization
            Bind<ICalendarSynchronizationService>().To<CalendarSynchronizationService>();
            Bind<IBoardItemsProcessor>().To<BoardItemsProcessor>();
            Bind<ISingleBoardItemProcessorFactory>().To<SingleBoardItemProcessorFactory>();
            Bind<IEventTimeFrameCreator>().To<EventTimeFrameCreator>();

            // Calendar Synchronization > Parsers
            Bind<IParser<BoardItemName>>().To<BoardItemNameParser>();
            Bind<IParser<Due>>().To<DueParser>();
            Bind<IParser<Location>>().To<LocationParser>();

            // Calendar Synchronization > Formatters
            Bind<ITimeFrameFormatter<Card>>().To<CardTimeFrameFormatter>();
            Bind<ISummaryFormatter<Card>>().To<CardSummaryFormatter>();
            Bind<ILocationFormatter<Card>>().To<CardLocationFormatter>();
            Bind<IDescriptionFormatter<Card>>().To<CardDescriptionFormatter>();
            Bind<IExtendedPropertiesFormatter<Card>>().To<CardExtendedPropertiesFormatter>();
            Bind<ITimeFrameFormatter<CheckItem>>().To<CheckItemTimeFrameFormatter>();
            Bind<ISummaryFormatter<CheckItem>>().To<CheckItemSummaryFormatter>();
            Bind<ILocationFormatter<CheckItem>>().To<CheckItemLocationFormatter>();
            Bind<IDescriptionFormatter<CheckItem>>().To<CheckItemDescriptionFormatter>();
            Bind<IExtendedPropertiesFormatter<CheckItem>>().To<CheckItemExtendedPropertiesFormatter>();

            // User Profile Synchronization
            Bind<IBoardCalendarBondSynchronizationService>().To<BoardCalendarBondSynchronizationService>();

            // Data Access
            Bind<IAccessTokenProviderFactory>().To<AccessTokenProviderFactory>();
            Bind<ICalendarAccessTokenExpirationHandler>().To<CalendarAccessTokenExpirationHandler>();

            // Time Zones
            Bind<ITimeZoneService>().To<TimeZoneService>();
        }
    }
}
