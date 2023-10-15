using Pathfinding.App.Console.Localization;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using System.Linq;

namespace Pathfinding.App.Console.Model
{
    internal sealed class Graph2DWrap : Graph<Vertex>
    {
        private const string LargeSpace = "   ";

        private int ObstaclesPercent => this.GetObstaclePercent();

        private int Obstacles => this.GetObstaclesCount();

        public Graph2DWrap(IGraph<Vertex> graph)
            : base(graph, graph.DimensionsSizes)
        {

        }

        public override string ToString()
        {
            var dimnsNames = new[] { Languages.WidthDimensionName, Languages.LengthDimensionName };
            var zipped = dimnsNames.Zip(DimensionsSizes, (n, s) => $"{n}: {s}");
            string joined = string.Join(LargeSpace, zipped);
            string graphParams = string.Format(Languages.GraphFormat, ObstaclesPercent, Obstacles, Count);
            return string.Join(LargeSpace, joined, graphParams);
        }
    }
}
