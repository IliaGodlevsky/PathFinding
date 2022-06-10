using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System.Collections.Generic;

namespace GraphLib.Extensions
{
    public static class IDictionaryExtensions
    {
        public static IVertex GetOrNullVertex<TKey>(this IReadOnlyDictionary<TKey, IVertex> self, TKey key)
        {
            return self.GetOrDefault(key, () => NullVertex.Instance);
        }
    }
}
