using System.Collections.Generic;
using System.Linq;

namespace Trellendar.Core.Extensions
{
    public static class EnumerableExtensions
    {
         public static bool IsNullOrEmpty<TItem>(this IEnumerable<TItem> collection)
         {
             return collection == null || !collection.Any();
         }

         public static bool IsNotNullOrEmpty<TItem>(this IEnumerable<TItem> collection)
         {
             return collection != null && collection.Any();
         }
    }
}