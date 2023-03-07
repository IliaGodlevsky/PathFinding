using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions
{
    file sealed class MatchComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> predicate;

        public MatchComparer(Func<T, T, bool> predicate)
        {
            this.predicate = predicate;
        }

        public bool Equals(T x, T y)
        {
            return predicate(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }

    public static class IEnumerableExtension
    {
        public static T AggregateOrDefault<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            return collection.Any() ? collection.Aggregate(func) : default;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
            return collection;
        }

        public static bool Juxtapose<T>(this IEnumerable<T> self, IEnumerable<T> second, Func<T, T, bool> predicate)
        {
            return self.SequenceEqual(second, new MatchComparer<T>(predicate));
        }

        public static bool Juxtapose<T>(this IEnumerable<T> self, IEnumerable<T> second)
        {
            return self.Juxtapose(second, (a, b) => a.Equals(b));
        }

        public static IEnumerable<T> Times<T>(this int count)
            where T : new()
        {
            while (count-- > 0) yield return new();
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, params T[] items)
        {
            return collection.Except(items.AsEnumerable());
        }

        public static IEnumerable<T> TakeOrDefault<T>(this IEnumerable<T> collection, int number, T defaultValue = default)
        {
            int count = 0;
            foreach (var item in collection)
            {
                count++;
                if (count > number)
                {
                    break;
                }
                yield return item;
            }
            int remained = count != number ? number - count : 0;
            while (remained-- > 0)
            {
                yield return defaultValue;
            }
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
        {
            return new(collection);
        }

        public static int ToHashCode(this IEnumerable<int> array)
        {
            return array.AggregateOrDefault(HashCode.Combine);
        }
    }
}
