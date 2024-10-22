using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Data.Extensions
{
    public static class DictionaryExtensions
    {
        public static IVertex GetOrNullVertex<TKey>(this IReadOnlyDictionary<TKey, IVertex> dictionary, TKey key)
        {
            return dictionary.GetOrDefault(key, NullVertex.Interface);
        }
    }
}
