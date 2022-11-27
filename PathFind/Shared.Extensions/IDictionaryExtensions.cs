using System;
using System.Collections;
using System.Collections.Generic;

namespace Shared.Extensions
{
    public static class IDictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key, Func<TValue> defaultValue)
        {
            return self.TryGetValue(key, out var value) ? value : defaultValue();
        }

        public static TValue GetOrEmpty<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key)
            where TValue : IEnumerable, new()
        {
            return self.GetOrDefault(key, () => new TValue());
        }

        public static TValue TryGetOrAddNew<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
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
    }
}
