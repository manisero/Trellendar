﻿using Ninject.Modules;
using Trellendar.DataAccess.Remote;
using Trellendar.Domain.Trello;
using Trellendar.Logic;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.CalendarSynchronization.Formatting;
using Trellendar.Logic.CalendarSynchronization.Formatting.Formatters;
using Trellendar.Logic.CalendarSynchronization.Parsers;
using Trellendar.Logic.CalendarSynchronization._Impl;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.DataAccess._Impl;
using Trellendar.Logic.Domain;
using Trellendar.Logic.TimeZones;
using Trellendar.Logic.TimeZones._Impl;
using Trellendar.Logic.UserProfileSynchronization;
using Trellendar.Logic.UserProfileSynchronization._Impl;
using Trellendar.Logic._Impl;

namespace Trellendar.WebSite.Ninject.ApplicationModules
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
            Bind<IUserProfileService>().To<UserProfileService>();

            // Data Access
            Bind<IAccessTokenProviderFactory>().To<AccessTokenProviderFactory>();
            Bind<ICalendarAccessTokenExpirationHandler>().To<CalendarAccessTokenExpirationHandler>();

            // Time Zones
            Bind<ITimeZoneService>().To<TimeZoneService>();
        }
    }
}
