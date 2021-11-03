using System.Collections.Concurrent;

namespace Common.Extensions.EnumerableExtensions
{
    public static class ConcurrentDictionaryExtensions
    {
        public static TValue TryGetOrAddNew<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key)
            where TValue : new()
        {
            if (self.TryGetValue(key, out var value))
            {
                return value;
            }

            value = new TValue();
            self.TryAdd(key, value);
            return value;
        }

        public static bool TryAddOrUpdate<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key, TValue value)
        {
            if (self.TryGetValue(key, out _))
            {
                self[key] = value;
                return true;
            }

            return self.TryAdd(key, value);
        }
    }
}