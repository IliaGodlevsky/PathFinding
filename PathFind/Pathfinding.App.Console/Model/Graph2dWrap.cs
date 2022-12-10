using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Pathfinding.App.Console.Model
{
    internal sealed class Graph2dWrap : Graph2D<Vertex>
    {
        private const string LargeSpace = "   ";
        private const string Format = "Obstacle percent: {0} ({1}/{2})";
        private static readonly string[] DimsNames = new[] { "Width", "Length" };

        private int ObstaclesPercent => this.GetObstaclePercent();

        private int Obstacles => this.GetObstaclesCount();

        public Graph2dWrap(Graph2D<Vertex> graph)
            : base(graph, graph.DimensionsSizes)
        {

        }

        public override string ToString()
        {
            var zipped = DimsNames.Zip(DimensionsSizes, (n, s) => $"{n}: {s}");
            string joined = string.Join(LargeSpace, zipped);
            string graphParams = string.Format(Format, ObstaclesPercent, Obstacles, Count);
            return string.Join(LargeSpace, joined, graphParams);
        }

        public override object Clone()
        {
            var graph = new Graph2D<Vertex>(vertices.Values.ToArray(), DimensionsSizes.ToArray());
            return new Graph2dWrap(graph);
        }
    }
}
