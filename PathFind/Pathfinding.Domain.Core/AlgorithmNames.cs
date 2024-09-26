using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Pathfinding.Domain.Core
{
    public static class AlgorithmNames
    {
        public static readonly ReadOnlyCollection<string> All;

        public static readonly string Dijkstra = Languages.DijkstraAlgorithm;
        public static readonly string AStar = Languages.AStarAlgorithm;
        public static readonly string IDAStar = Languages.IDAStarAlgorithm;
        public static readonly string Random = Languages.RandomAlgorithm;
        public static readonly string Lee = Languages.LeeAlgorithm;
        public static readonly string AStarLee = Languages.AStarLeeAlgorithm;
        public static readonly string Depth = Languages.DepthFirstAlgorithm;
        public static readonly string Cost = Languages.CostGreedyAlgorithm;

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
