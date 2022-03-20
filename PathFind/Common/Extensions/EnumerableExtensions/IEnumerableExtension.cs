using Common.Attrbiutes;
using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public static IEnumerable<T> ForAll<T>(this IEnumerable<T> collection, Action<T> action)
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

        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>
            (this IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            return collection.ToDictionary(item => item.Key, item => item.Value);
        }

        public static IDictionary<string, T> ToNameInstanceDictionary<T>(this IEnumerable<T> collection)
        {
            return collection.ToDictionary(item => item.GetDescription());
        }

        public static Tuple<string, T>[] ToNameInstanceTuples<T>(this IEnumerable<T> collection)
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

        public static IEnumerable<T> GroupByGroupAttribute<T>(this IEnumerable<T> collection)
        {
            return collection
                .GroupBy(item => item.GetAttributeOrNull<GroupAttribute>())
                .Select(item => item.OrderByOrderAttribute())
                .SelectMany(item => item);
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

        public static IEnumerable<T> TakeOrDefault<T>(this IEnumerable<T> collection, int number)
        {
            int count = 0;
            using (var iterator = collection.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    count++;
                    yield return iterator.Current;
                }
            }
            if (count != number)
            {
                int remained = number - count;
                while (remained-- > 0)
                {
                    yield return default;
                }
            }
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
        {
            return new Queue<T>(collection);
        }

        public static ReadOnlyCollection<T> ToReadOnly<T>(this IList<T> collection)
        {
            return new ReadOnlyCollection<T>(collection);
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
