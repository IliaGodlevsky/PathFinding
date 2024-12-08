using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class HeuristicsExtensions
    {
        public static string ToStringRepresentation(this HeuristicFunctions heuristics)
        {
            return heuristics switch
            {
                HeuristicFunctions.Euclidian => "Euclidian",
                HeuristicFunctions.Chebyshev => "Chebyshev",
                HeuristicFunctions.Diagonal => "Diagonal",
                HeuristicFunctions.Manhattan => "Manhattan",
                HeuristicFunctions.Cosine => "Cosine",
                _ => "",
            };
        }
    }
}
