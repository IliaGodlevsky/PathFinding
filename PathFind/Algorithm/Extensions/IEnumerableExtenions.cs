using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class IEnumerableExtenions
    {
        internal static IVertex FirstOrDefault(this IEnumerable<IVertex> collection)
        {
            return !collection.Any() ? new DefaultVertex() : collection.First();
        }

        internal static IEnumerable<T> Except<T>(this IEnumerable<T> collection, params T[] objects)
        {
            return collection.Except(objects.AsEnumerable());
        }

        internal static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach(var item in collection)
            {
                action(item);
            }

            return collection;
        }
    }
}
