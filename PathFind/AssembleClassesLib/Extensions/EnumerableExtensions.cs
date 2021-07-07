using System.Collections.Generic;
using System.Linq;

namespace AssembleClassesLib.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns a dictionary of class name as key and 
        /// class instance as value from <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IDictionary<string, T> AsNameInstanceDictionary<T>(this IEnumerable<T> collection)
        {
            return collection.ToDictionary(item => item.GetClassName());
        }
    }
}
