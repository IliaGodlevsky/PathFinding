using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class AlgorithmExtensions
    {
        public static string ToStringRepresentation(this Algorithms algorithm)
        {
            return algorithm switch
            {
                Algorithms.Dijkstra => "Dijkstra",
                Algorithms.BidirectDijkstra => "Bi Dijkstra",
                Algorithms.AStar => "A*",
                Algorithms.BidirectAStar => "Bidirect A*",
                Algorithms.Lee => "Lee",
                Algorithms.BidirectLee => "Bidirect Lee",
                Algorithms.AStarLee => "A* Lee",
                Algorithms.DistanceFirst => "DFS dist",
                Algorithms.CostGreedy => "DFS cost",
                Algorithms.AStarGreedy => "A* greedy",
                Algorithms.DepthFirst => "DFS",
                Algorithms.Snake => "Snake",
                _ => ""
            };
        }
    }
}
