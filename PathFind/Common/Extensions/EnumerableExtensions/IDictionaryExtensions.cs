using System;
using System.Collections.Generic;

namespace Common.Extensions.EnumerableExtensions
{
    public static class IDictionaryExtensions
    {
        public static TValue TryGetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
        where TValue : new()
        {
            if (self.TryGetValue(key, out var value))
            {
                return value;
            }

            value = new TValue();
            self.Add(key, value);
            return value;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, Func<TValue> defaultValue)
        {
            if (self.TryGetValue(key, out var value))
            {
                return value;
            }

            return defaultValue();
        }

        public static HashSet<T> GetOrEmpty<TKey, T>(this IDictionary<TKey, HashSet<T>> self, TKey key)
        {
            return self.GetOrDefault(key, () => new HashSet<T>());
        }

        public static List<T> GetOrEmpty<TKey, T>(this IDictionary<TKey, List<T>> self, TKey key)
        {
            return self.GetOrDefault(key, () => new List<T>());
        }
    }
}
