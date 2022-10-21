using Common.Attrbiutes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Common.Extensions.EnumerableExtensions
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

        public static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> collection)
        {
            switch (collection)
            {
                case ReadOnlyCollection<T> readOnly: return readOnly;
                case IList<T> list: return new ReadOnlyCollection<T>(list);
                default: return collection.ToList().AsReadOnly();
            }
        }

        public static IOrderedEnumerable<T> OrderByOrderAttribute<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(item => item.GetAttributeOrNull<OrderAttribute>()?.Order ?? OrderAttribute.Default.Order);
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
            return array.AggregateOrDefault(HashCode.Combine);
        }
    }
}
