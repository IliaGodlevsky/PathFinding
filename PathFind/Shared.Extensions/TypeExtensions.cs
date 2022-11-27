using System;

namespace Shared.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines, whether <paramref name="type"/>
        /// implements <typeparamref name="TInterface"/> interface
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="type"></param>
        /// <returns>true if <paramref name="type"/> implements 
        /// <typeparamref name="TInterface"/>interface, otherwise - false</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="System.Reflection.AmbiguousMatchException"></exception>
        public static bool Implements<TInterface>(this Type type)
               where TInterface : class
        {
            return type.GetInterface(typeof(TInterface).Name) != null;
        }
    }
}
