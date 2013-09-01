using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Trellendar.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<TypeInfo> GetConcreteTypes<TBaseType>(this Assembly assembly)
        {
            return assembly.DefinedTypes.Where(x => x.IsClass && !x.IsAbstract && typeof(TBaseType).IsAssignableFrom(x));
        }
    }
}
