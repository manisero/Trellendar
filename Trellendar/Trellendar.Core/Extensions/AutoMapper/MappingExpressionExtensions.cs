using System;
using System.Linq.Expressions;
using AutoMapper;

namespace Trellendar.Core.Extensions.AutoMapper
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> Map<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression, 
            Expression<Func<TSource, object>> sourceMember,
            Expression<Func<TDestination, object>> destinationMember)
        {
            return expression.ForMember(destinationMember, o => o.MapFrom(sourceMember));
        }

        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression,
                                                                                              Expression<Func<TDestination, object>> member)
        {
            return expression.ForMember(member, o => o.Ignore());
        }
    }
}
