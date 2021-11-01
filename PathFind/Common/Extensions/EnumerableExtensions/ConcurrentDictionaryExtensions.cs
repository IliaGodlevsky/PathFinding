using System.Collections.Concurrent;

namespace Common.Extensions.EnumerableExtensions
{
    public static class ConcurrentDictionaryExtensions
    {
        public static TValue TryGetOrAdd<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key)
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
    }
}
