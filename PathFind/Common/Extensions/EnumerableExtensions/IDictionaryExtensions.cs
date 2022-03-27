using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.Extensions.EnumerableExtensions
{
    public static class IDictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key, Func<TValue> defaultValue)
        {
            if (self.TryGetValue(key, out var value))
            {
                return value;
            }

            return defaultValue();
        }

        public static TValue GetOrEmpty<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key)
            where TValue : IEnumerable, new()
        {
            return self.GetOrDefault(key, () => new TValue());
        }
    }
}
