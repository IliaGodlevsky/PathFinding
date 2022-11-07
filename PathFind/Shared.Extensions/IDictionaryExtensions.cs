using Shared.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    }
}
