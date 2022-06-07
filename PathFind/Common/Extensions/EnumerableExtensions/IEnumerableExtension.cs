using Common.Attrbiutes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Common.Extensions.EnumerableExtensions
{
    sealed class MatchComparer<T> : IEqualityComparer<T>
    {
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

        private readonly Func<T, T, bool> predicate;
    }

    public static class IEnumerableExtension
    {
        public static T AggregateOrDefault<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            return collection.Any() ? collection.Aggregate(func) : default;
        }

        public static IEnumerable<T> Without<T>(this IEnumerable<T> self, IEnumerable<T> items)
        {
            return self.Where(item => !items.Contains(item));
        }

        public static IEnumerable<T> Without<T>(this IEnumerable<T> self, params T[] items)
        {
            return self.Without(items.AsEnumerable());
        }

        public static int GetMultiplication(this IEnumerable<int> array)
        {
            return array.AggregateOrDefault((x, y) => x * y);
        }

        public static T ForAll<T, U>(this T collection, Action<U> action)
            where T : IEnumerable<U>
        {
            collection.ForEach(action);
            return collection;
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static bool IsSingle<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Count(predicate) == 1;
        }

        public static double SumOrDefault(this IEnumerable<double> collection)
        {
            return collection.Any() ? collection.Sum() : default;
        }

        public static int SumOrDefault(this IEnumerable<int> collection)
        {
            return collection.Any() ? collection.Sum() : default;
        }

        public static double MaxOrDefault(this IEnumerable<double> collection)
        {
            return collection.Any() ? collection.Max() : default;
        }

        public static double MinOrDefault<T>(this IEnumerable<T> collection, Func<T, double> selector)
        {
            return collection.Any() ? collection.Min(selector) : default;
        }

        public static bool Juxtapose<T>(this IEnumerable<T> self, IEnumerable<T> second, Func<T, T, bool> predicate)
        {
            return self.SequenceEqual(second, new MatchComparer<T>(predicate));
        }

        public static bool Juxtapose<T>(this IEnumerable<T> self, IEnumerable<T> second)
        {
            return self.Juxtapose(second, (a, b) => a.Equals(b));
        }

        public static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>
            (this IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            return new ReadOnlyDictionary<TKey, TValue>(collection.ToDictionary(item => item.Key, item => item.Value));
        }

        public static Dictionary<string, T> ToNameInstanceDictionary<T>(this IEnumerable<T> collection)
        {
            return collection.ToDictionary(item => item.GetDescription());
        }

        public static IReadOnlyList<Tuple<string, T>> ToNameInstanceTuples<T>(this IEnumerable<T> collection)
        {
            return collection
                .Select(item => new Tuple<string, T>(item.GetDescription(), item))
                .ToArray();
        }

        public static IEnumerable<T1> GetItems1<T1, T2>(this IEnumerable<Tuple<T1, T2>> tuples)
        {
            return tuples.Select(item => item.Item1);
        }

        public static IOrderedEnumerable<T> OrderByOrderAttribute<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(item => item.GetOrder());
        }

        public static IOrderedEnumerable<MethodInfo> OrderByOrderAttribute(this IEnumerable<MethodInfo> collection)
        {
            return collection.OrderBy(item => item.GetOrder());
        }

        public static IEnumerable<T> GroupByGroupAttribute<T>(this IEnumerable<T> collection)
        {
            return collection
                .GroupBy(item => item.GetAttributeOrNull<GroupAttribute>())
                .SelectMany(item => item.OrderByOrderAttribute());
        }

        public static IList<T> Shuffle<T>(this IList<T> list, Func<int> randomFunction)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int index = randomFunction() % list.Count;
                var temp = list[i];
                list[i] = list[index];
                list[index] = temp;
            }
            return list;
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
            return new Queue<T>(collection);
        }

        public static int ToHashCode(this IEnumerable<int> array)
        {
            return array.AggregateOrDefault(IntExtensions.Xor);
        }

        public static bool ContainsUniqueValues<T>(this IEnumerable<T> collection)
        {
            return collection.ContainsUniqueValues(EqualityComparer<T>.Default);
        }

        public static bool ContainsUniqueValues<T>(this IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            return collection.Distinct(comparer).Count() == collection.Count();
        }
    }
}
