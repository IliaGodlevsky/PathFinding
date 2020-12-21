using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class IEnumerableExtenions
    {
        /// <summary>
        /// Returns first element if collection isn't empty and <see cref="DefaultVertex"/> if is
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>First element of collection or <see cref="DefaultVertex"/> if collection is empty</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="collection"/> is null</exception>
        internal static IVertex FirstOrDefault(this IEnumerable<IVertex> collection)
        {
            return collection.FirstOrDefault<IVertex>() ?? new DefaultVertex();
        }

        /// <summary>
        /// Returns collection withour <paramref name="objects"/> using default comparator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="objects"></param>
        /// <returns>Collection without <paramref name="objects"/></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        internal static IEnumerable<T> Except<T>(this IEnumerable<T> collection, params T[] objects)
        {
            return collection.Except(objects.AsEnumerable());
        }
    }
}
