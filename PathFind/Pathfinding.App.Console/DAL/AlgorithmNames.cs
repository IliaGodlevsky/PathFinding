using Pathfinding.App.Console.Localization;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL
{
    internal static class AlgorithmNames
    {
        public static readonly IReadOnlyCollection<string> Algorithms =
            new List<string>() { Dijkstra, AStar, IDAStar, Random, Lee, AStarLee, Depth, Cost };

        public const string Dijkstra = nameof(Languages.DijkstraAlgorithm);
        public const string AStar = nameof(Languages.AStarAlgorithm);
        public const string IDAStar = nameof(Languages.IDAStarAlgorithm);
        public const string Random = nameof(Languages.RandomAlgorithm);
        public const string Lee = nameof(Languages.LeeAlgorithm);
        public const string AStarLee = nameof(Languages.AStarLeeAlgorithm);
        public const string Depth = nameof(Languages.DepthFirstAlgorithm);
        public const string Cost = nameof(Languages.CostGreedyAlgorithm);
    }
}
