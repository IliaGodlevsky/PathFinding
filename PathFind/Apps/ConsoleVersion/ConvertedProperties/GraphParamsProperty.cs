using ConsoleVersion.Model;
using GraphLib.Extensions;
using GraphLib.Realizations.Graphs;
using System.Linq;

namespace ConsoleVersion.ConvertedProperties
{
    internal sealed class GraphParamsProperty : ConvertedProperty<Graph2D<Vertex>, string>
    {
        private const string LargeSpace = "   ";
        private const string Format = "Obstacle percent: {0} ({1}/{2})";
        private static readonly string[] DimensionNames = new[] { "Width", "Length" };

        public static readonly GraphParamsProperty Empty
            = new GraphParamsProperty(Graph2D<Vertex>.Empty);

        public static GraphParamsProperty Assign(Graph2D<Vertex> graph)
            => new GraphParamsProperty(graph);

        private GraphParamsProperty(Graph2D<Vertex> graph) : base(graph)
        {

        }

        protected override string ConvertTo(Graph2D<Vertex> graph)
        {
            if (graph != Graph2D<Vertex>.Empty)
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
