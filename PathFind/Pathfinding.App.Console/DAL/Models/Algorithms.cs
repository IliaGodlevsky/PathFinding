using Pathfinding.App.Console.Localization;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Models
{
    internal static class Algorithms
    {
        public static readonly IReadOnlyCollection<string> Algos;

        public const string Dijkstra = nameof(Languages.DijkstraAlgorithm);
        public const string AStar = nameof(Languages.AStarAlgorithm);
        public const string IDAStar = nameof(Languages.IDAStarAlgorithm);
    }
}
