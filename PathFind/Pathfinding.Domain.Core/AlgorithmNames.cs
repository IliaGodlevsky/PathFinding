using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Pathfinding.Domain.Core
{
    public static class AlgorithmNames
    {
        public static readonly ReadOnlyCollection<string> All;

        public const string Dijkstra = nameof(Languages.DijkstraAlgorithm);
        public const string AStar = nameof(Languages.AStarAlgorithm);
        public const string IDAStar = nameof(Languages.IDAStarAlgorithm);
        public const string Random = nameof(Languages.RandomAlgorithm);
        public const string Lee = nameof(Languages.LeeAlgorithm);
        public const string AStarLee = nameof(Languages.AStarLeeAlgorithm);
        public const string Depth = nameof(Languages.DepthFirstAlgorithm);
        public const string Cost = nameof(Languages.CostGreedyAlgorithm);

        static AlgorithmNames()
        {
            All = typeof(AlgorithmNames)
                .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
                .Where(field => field.FieldType == typeof(string))
                .Select(field => (string)field.GetValue(null))
                .ToList().AsReadOnly();
        }
    }
}
