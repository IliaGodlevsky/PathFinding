using System.Collections.Concurrent;

namespace Common.Extensions.EnumerableExtensions
{
    public static class ConcurrentDictionaryExtensions
    {
        /// <summary>
        /// Tries get value or adds a new one if value does not exist
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns>A value if it exists and a new value if doesn't</returns>
        /// <remarks><typeparamref name="TValue"/> must have a default constructor</remarks>
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

        /// <summary>
        /// Tries add value if is doesn't exist or update it if it does
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>true if value was added or updated successfully</returns>
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