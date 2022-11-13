using Shared.Collections;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class IEnumerableExtension
    {
        private sealed class MatchComparer<T> : IEqualityComparer<T>
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

        public static T AggregateOrDefault<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            return collection.Any() ? collection.Aggregate(func) : default;
        }

        public static async IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                await Task.CompletedTask;
                yield return item;
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
            return collection;
        }

        public static IEnumerable<T> AppendRange<T>(this IEnumerable<T> collection, params T[] range)
        {
            var temp = collection;
            foreach (var item in range)
            {
                temp = temp.Append(item);
            }
            return temp;
        }

        public static bool Juxtapose<T>(this IEnumerable<T> self, IEnumerable<T> second, Func<T, T, bool> predicate)
        {
            return self.SequenceEqual(second, new MatchComparer<T>(predicate));
        }

        public static bool Juxtapose<T>(this IEnumerable<T> self, IEnumerable<T> second)
        {
            return self.Juxtapose(second, (a, b) => a.Equals(b));
        }

        public static ReadOnlyList<T> ToReadOnly<T>(this IEnumerable<T> collection)
        {
            switch (collection)
            {
                case ReadOnlyList<T> readOnly: return readOnly;
                case IList<T> list: return new ReadOnlyList<T>(list);
                default: return new ReadOnlyList<T>(collection.ToArray());
            }
        }

        public static IOrderedEnumerable<T> Shuffle<T>(this IEnumerable<T> collection, Func<int> selector)
        {
            return collection.OrderBy(_ => selector());
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

        public static ReadOnlyDictionary<TKey, TValue> ToReadOnly<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            switch (collection)
            {
                case IDictionary<TKey, TValue> dictionary:
                    return new ReadOnlyDictionary<TKey, TValue>(dictionary);
                case ReadOnlyDictionary<TKey, TValue> readOnly:
                    return readOnly;
                default:
                    var dict = collection.ToDictionary(item => item.Key, item => item.Value);
                    return new ReadOnlyDictionary<TKey, TValue>(dict);
            }
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
        {
            return new Queue<T>(collection);
        }

        public static int ToHashCode(this IEnumerable<int> array)
        {
            return array.AggregateOrDefault(HashCode.Combine);
        }
    }
}
