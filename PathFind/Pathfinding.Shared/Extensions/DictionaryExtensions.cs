using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary switch
            {
                IDictionary<TKey, TValue> d => new Dictionary<TKey, TValue>(d),
                _ => dictionary.ToDictionary(item => item.Key, item => item.Value),
            };
        }
    }
}
