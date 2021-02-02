using Algorithm.Handlers;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System;
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
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="collection"/> is null</exception>
        public static IVertex FirstOrDefault(this IEnumerable<IVertex> collection)
        {
            return collection.FirstOrDefault<IVertex>() ?? new DefaultVertex();
        }

        public static double Min(this IEnumerable<IVertex> collection, HeuristicHandler heuristicFunction)
        {
            double selector(IVertex vertex) => heuristicFunction(vertex);
            return collection.Min((Func<IVertex, double>)selector);
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
        {
            return new Queue<T>(collection);
        }
    }
}
