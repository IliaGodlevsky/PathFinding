﻿using Pathfinding.App.Console.Localization;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System.Linq;

namespace Pathfinding.App.Console.Model
{
    internal sealed class Graph2DWrap : Graph2D<Vertex>
    {
        private const string LargeSpace = "   ";

        private int ObstaclesPercent => this.GetObstaclePercent();

        private int Obstacles => this.GetObstaclesCount();

        public Graph2DWrap(Graph2D<Vertex> graph)
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

        public override Graph2D<Vertex> Clone()
        {
            var graph = new Graph2D<Vertex>(vertices.Values.ToArray(), DimensionsSizes.ToArray());
            return new Graph2DWrap(graph);
        }
    }
}
