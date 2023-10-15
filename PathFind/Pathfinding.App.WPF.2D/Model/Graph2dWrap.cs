using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using System.Linq;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Graph2dWrap : Graph<Vertex>
    {
        private const string LargeSpace = "   ";
        private const string Format = "Obstacle percent: {0} ({1}/{2})";
        private static readonly string[] DimsNames = new[] { "Width", "Length" };

        private int ObstaclesPercent => this.GetObstaclePercent();

        private int Obstacles => this.GetObstaclesCount();

        public Graph2dWrap(IGraph<Vertex> graph)
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
    }
}
