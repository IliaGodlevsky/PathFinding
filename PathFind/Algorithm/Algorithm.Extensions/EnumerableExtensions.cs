using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class EnumerableExtensions
    {
        public static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
        {
            return new Queue<T>(collection);
        }

        public static IVertex FirstOrNullVertex(this IEnumerable<IVertex> collection, Func<IVertex, bool> predicate)
        {
            return collection.FirstOrDefault(predicate) ?? new NullVertex();
        }
    }
}
