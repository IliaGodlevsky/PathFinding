﻿using Pathfinding.ConsoleApp.Resources;
using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class HeuristicsExtensions
    {
        public static string ToStringRepresentation(this HeuristicFunctions heuristics)
        {
            return heuristics switch
            {
                HeuristicFunctions.Euclidian => Resource.Euclidian,
                HeuristicFunctions.Chebyshev => Resource.Chebyshev,
                HeuristicFunctions.Diagonal => Resource.Diagonal,
                HeuristicFunctions.Manhattan => Resource.Manhattan,
                HeuristicFunctions.Cosine => Resource.Cosine,
                _ => string.Empty,
            };
        }
    }
}
