using System;
using System.Collections.Generic;
using Trellendar.Core.DependencyResolution;
using Trellendar.Domain.Trello;
using Trellendar.Core.Extensions;
using Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class SingleBoardItemProcessorFactory : ISingleBoardItemProcessorFactory
    {
        private readonly IDependencyResolver _dependencyResolver;

        private readonly IDictionary<Type, Func<object>> _processors = new Dictionary<Type, Func<object>>();

        public SingleBoardItemProcessorFactory(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;

            _processors[typeof(Card)] = () => new CardProcessor(_dependencyResolver.Resolve<UserContext>(),
                                                                _dependencyResolver.Resolve<IEventTimeFrameCreator>(),
                                                                _dependencyResolver.Resolve<IParser<Due>>());

            _processors[typeof(CheckItem)] = () => new CheckItemProcessor(_dependencyResolver.Resolve<UserContext>(),
                                                                          _dependencyResolver.Resolve<IParser<Due>>(),
                                                                          _dependencyResolver.Resolve<IEventTimeFrameCreator>());
        }

        public ISingleBoardItemProcessor<TItem> Create<TItem>()
        {
            var itemType = typeof(TItem);

            if (!_processors.ContainsKey(itemType))
            {
                throw new NotSupportedException("No processor for '{0}' found".FormatWith(itemType.Name));
            }

            return (ISingleBoardItemProcessor<TItem>)_processors[itemType]();
        }
    }
}