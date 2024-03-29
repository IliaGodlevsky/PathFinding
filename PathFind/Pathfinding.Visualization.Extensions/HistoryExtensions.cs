﻿using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Linq;

namespace Pathfinding.Visualization.Extensions
{
    public static class HistoryExtensions
    {
        public static void VisualizeHistory<TVertex, TVolume>(this IHistoryRepository<TVolume> history, Guid key, IGraph<TVertex> graph)
            where TVertex : IVertex, ITotallyVisualizable
            where TVolume : IHistoryVolume<ICoordinate>
        {
            history.GetObstacles(key).Select(graph.Get).ForEach(v => v.VisualizeAsObstacle());
            history.GetVisited(key).Select(graph.Get).ForEach(v => v.VisualizeAsVisited());
            history.GetPathfindingRange(key).Select(graph.Get).Reverse().VisualizeAsRange();
            history.GetPath(key).Select(graph.Get).ForEach(v => v.VisualizeAsPath());
        }
    }
}
