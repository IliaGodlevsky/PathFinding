﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Visualization.Extensions
{
    internal static class IGraphExtensions
    {
        public static void RemoveAllColors<TGraph, TVertex>(this TGraph graph)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex, IVisualizable
        {
            graph.ForEach(vertex => vertex.VisualizeAsRegular());
        }

        public static IEnumerable<TVertex> GetObstacles<TGraph, TVertex>(this TGraph graph)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex, IVisualizable
        {
            return graph.Where(vertex => vertex.IsObstacle);
        }
    }
}