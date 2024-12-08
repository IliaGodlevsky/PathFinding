﻿using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Data.Extensions
{
    public static class DictionaryExtensions
    {
        public static IPathfindingVertex GetOrNullVertex<TKey>(this IReadOnlyDictionary<TKey, IPathfindingVertex> dictionary, TKey key)
        {
            return dictionary.GetOrDefault(key, NullPathfindingVertex.Interface);
        }
    }
}
