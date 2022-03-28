using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System.Collections.Generic;

namespace GraphLib.Extensions
{
    public static class IDictionaryExtensions
    {
        public static IVertex GetOrNullVertex<TKey>(this IReadOnlyDictionary<TKey, IVertex> self, TKey key)
        {
            if (self.TryGetValue(key, out var value))
            {
                return value;
            }

            return NullVertex.Instance;
        }
    }
}
