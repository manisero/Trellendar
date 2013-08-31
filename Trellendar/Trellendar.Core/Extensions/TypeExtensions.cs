using System;

namespace Trellendar.Core.Extensions
{
    public static class TypeExtensions
    {
        public static TInstance CreateInstance<TInstance>(this Type type)
        {
            return (TInstance)Activator.CreateInstance(type);
        }
    }
}
