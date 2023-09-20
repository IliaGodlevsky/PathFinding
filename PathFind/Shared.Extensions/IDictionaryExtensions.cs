using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Shared.Extensions
{
    public static class IDictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key, Func<TValue> defaultValue)
        {
            return self.TryGetValue(key, out var value) ? value : defaultValue();
        }

        public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key, TValue defaultValue = default)
        {
            return self.GetOrDefault(key, () => defaultValue);
        }

        public static TValue GetOrEmpty<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key)
            where TValue : IEnumerable, new()
        {
            return self.GetOrDefault(key, () => new());
        }

        public static TValue TryGetOrAddNew<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
            where TValue : new()
        {
            if (self.TryGetValue(key, out var value))
            {
                return value;
            }

            value = new();
            self.Add(key, value);
            return value;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.ToDictionary(item => item.Key, item => item.Value);
        }

        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new (dictionary);
        }
    }
}
