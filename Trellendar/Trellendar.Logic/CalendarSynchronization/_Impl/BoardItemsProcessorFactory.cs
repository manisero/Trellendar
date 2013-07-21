using System;
using System.Collections.Generic;
using Trellendar.Core.DependencyResolution;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.BoardItemsProcessors;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class BoardItemsProcessorFactory : IBoardItemsProcessorFactory
    {
        private readonly IDependencyResolver _dependencyResolver;

        private readonly IDictionary<Type, Func<object>> _processors = new Dictionary<Type, Func<object>>();

        public BoardItemsProcessorFactory(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;

            _processors[typeof(Card)] = () => new CardsProcessor(_dependencyResolver.Resolve<UserContext>(), _dependencyResolver.Resolve<ICalendarAPI>());
        }

        public IBoardItemsProcessor<TItem> Create<TItem>()
        {
            var itemType = typeof(TItem);

            if (!_processors.ContainsKey(itemType))
            {
                throw new NotSupportedException("No processor for '{0}' found".FormatWith(itemType.Name));
            }

            return (IBoardItemsProcessor<TItem>)_processors[itemType]();
        }
    }
}