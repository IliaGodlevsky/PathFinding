using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Pathfinding.Infrastructure.Data
{
    internal static class DbTables
    {
        public static readonly ReadOnlyCollection<string> All;

        public const string Graphs = "Graphs";
        public const string Algorithms = "Algorithms";
        public const string Ranges = "Ranges";
        public const string Vertices = "Vertices";
        public const string Neighbors = "Neighbors";
        public const string SubAlgorithms = "SubAlgorithms";
        public const string AlgorithmRuns = "AlgorithmRuns";
        public const string GraphStates = "GraphStates";
        public const string Statistics = "Statistics";

        static DbTables()
        {
            All = typeof(DbTables)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .Where(x => x.FieldType == typeof(string))
                .Select(x => (string)x.GetValue(null))
                .ToList()
                .AsReadOnly();
        }
    }
}
