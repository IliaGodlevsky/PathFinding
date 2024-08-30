using Pathfinding.Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Shared.Extensions
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

    public static class EnumerableExtension
    {
        public static IOrderedEnumerable<T> OrderByOrderAttribute<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(item => item.GetOrder());
        }

        public static IReadOnlyList<T> ToReadOnly<T>(this IEnumerable<T> collection)
        {
            return collection switch
            {
                ReadOnlyCollection<T> readOnly => readOnly,
                _ => Array.AsReadOnly(collection.ToArray()),
            };
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            items.ForEach(collection.Add);
        }

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

        public static IEnumerable<T> ForWhole<T>(this IEnumerable<T> collection, Action<IEnumerable<T>> action)
        {
            action(collection);
            return collection;
        }

        public static T[] ForWhole<T>(this T[] collection, Action<T[]> action)
        {
            action(collection);
            return collection;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            int i = 0;
            foreach (var item in collection)
            {
                action(item, i);
                i++;
            }
            return collection;
        }

        //public static bool Juxtapose<T>(this IEnumerable<T> self, IEnumerable<T> second, Func<T, T, bool> predicate)
        //{
        //    return self.SequenceEqual(second, new MatchComparer<T>(predicate));
        //}

        public static bool Juxtapose<T, U>(this IEnumerable<T> self, IEnumerable<U> second, Func<T, U, bool> predicate)
        {
            using (var enumerator = self.GetEnumerator())
            {
                using var enumerator2 = second.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (!enumerator2.MoveNext() || !predicate(enumerator.Current, enumerator2.Current))
                    {
                        return false;
                    }
                }

                if (enumerator2.MoveNext())
                {
                    return false;
                }
            }
            return true;
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

        public static T To<T>(this IEnumerable<T> items, Func<IEnumerable<T>, T> selector)
        {
            return selector(items);
        }

        public static async Task<T> ToAsync<T>(this IEnumerable<T> items,
            Func<IEnumerable<T>, CancellationToken, Task<T>> selector, 
            CancellationToken token = default)
        {
            return await selector(items, token);
        }

        public static async Task<U> ToAsync<T, U>(this IEnumerable<T> items,
            Func<IEnumerable<T>, CancellationToken, Task<U>> selector,
            CancellationToken token = default)
        {
            return await selector(items, token);
        }

        public static U To<T, U>(this IEnumerable<T> items, Func<IEnumerable<T>, U> selector)
        {
            return selector(items);
        }


    }
}
