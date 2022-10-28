using Common.ReadOnly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions.EnumerableExtensions
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
    }
}
