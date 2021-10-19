using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Creates a new queue from specified collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns>An instance of queue, that contains 
        /// elements from the specified collection</returns>
        public static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
        {
            return new Queue<T>(collection);
        }

        /// <summary>
        /// Returns the first vertex of the sequence using <paramref name="predicate"/> 
        /// of returns <see cref="NullVertex"/> if the sequence is empty or 
        /// no element passes the <paramref name="predicate"/>
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IVertex FirstOrNullVertex(this IEnumerable<IVertex> collection, Func<IVertex, bool> predicate)
        {
            return collection.FirstOrDefault(predicate) ?? new NullVertex();
        }
    }
}
