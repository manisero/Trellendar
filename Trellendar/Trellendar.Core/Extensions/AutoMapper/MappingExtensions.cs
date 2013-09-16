using System.Collections;
using System.Collections.Generic;
using AutoMapper;

namespace Trellendar.Core.Extensions.AutoMapper
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TDestination>(this object source)
        {
            return (TDestination)Mapper.Map(source, source.GetType(), typeof(TDestination));
        }

        public static IEnumerable<TDestination> MapTo<TDestination>(this IEnumerable source)
        {
            return (IEnumerable<TDestination>)Mapper.Map(source, source.GetType(), typeof(IEnumerable<TDestination>));
        }

        public static TDestination MapFrom<TDestination, TSource>(this TDestination destination, TSource source)
        {
            return Mapper.Map(source, destination);
        }
    }
}
