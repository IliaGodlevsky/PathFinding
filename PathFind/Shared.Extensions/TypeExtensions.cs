using System;

namespace Shared.Extensions
{
    public static class TypeExtensions
    {
        public static bool Implements<TInterface>(this Type type)
               where TInterface : class
        {
            return type.GetInterface(typeof(TInterface).Name) != null;
        }
    }
}
