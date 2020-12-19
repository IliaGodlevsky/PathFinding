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

        internal static Dictionary<TKey, TValue> ForEach<TKey, TValue>(
            this Dictionary<TKey, TValue> collection, 
            Action<TValue> action)
        {
            foreach(var value in collection.Values)
            {
                action(value);
            }

            return collection;
        }

        internal static IEnumerable<T> Except<T>(this IEnumerable<T> collection, params T[] objects)
        {
            return collection.Except(objects.AsEnumerable());
        }
    }
}
