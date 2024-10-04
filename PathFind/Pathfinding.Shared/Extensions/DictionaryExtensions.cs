using Pathfinding.Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pathfinding.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key, Func<TValue> defaultValue)
        {
            return self.TryGetValue(key, out var value) ? value : defaultValue();
        }

        public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key, TValue defaultValue = default)
        {
            return self.GetOrDefault(key, () => defaultValue);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            switch (dictionary)
            {
                case IDictionary<TKey, TValue> d: return new Dictionary<TKey, TValue>(d);
                default: return dictionary.ToDictionary(item => item.Key, item => item.Value);
            }
        }

        public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    }
}
