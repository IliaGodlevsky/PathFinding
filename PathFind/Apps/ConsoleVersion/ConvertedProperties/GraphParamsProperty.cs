using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using NullObject.Extensions;
using System.Linq;

namespace ConsoleVersion.ConvertedProperties
{
    internal sealed class GraphParamsProperty : ConvertedProperty<IGraph, string>
    {
        private const string LargeSpace = "   ";
        private const string Format = "Obstacle percent: {0} ({1}/{2})";
        private static readonly string[] DimensionNames = new[] { "Width", "Length" };

        public static readonly GraphParamsProperty Empty
            = new GraphParamsProperty(NullGraph.Instance);

        public static GraphParamsProperty Assign(IGraph graph)
            => new GraphParamsProperty(graph);

        private GraphParamsProperty(IGraph graph) : base(graph)
        {

        }

        protected override string ConvertTo(IGraph graph)
        {
            if (!graph.IsNull())
            {
                int obstacles = graph.GetObstaclesCount();
                int obstaclesPercent = graph.GetObstaclePercent();
                string Zip(string name, int size) => $"{name}: {size}";
                var zipped = DimensionNames.Zip(graph.DimensionsSizes, Zip);
                string joined = string.Join(LargeSpace, zipped);
                string graphParams = string.Format(Format,
                    obstaclesPercent, obstacles, graph.Count);
                return string.Join(LargeSpace, joined, graphParams);
            }
            return string.Empty;
        }
    }
}
