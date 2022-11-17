using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
using Pathfinding.AlgorithmLib.History;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System;

namespace Pathfinding.Visualization.Extensions
{
    public static class HistoryExtensions
    {
        public static void VisualizeHistory<TVertex, TVolume>(this History<TVolume> history, Guid key, IGraph<TVertex> graph)
            where TVertex : IVertex, IVisualizable
            where TVolume : IHistoryVolume<ICoordinate>, new()
        {
            graph.GetVertices(history.GetPath(key)).VisualizeAsPath();
            graph.GetVertices(history.GetVisitedVertices(key)).VisualizeAsVisited();
            graph.GetVertices(history.GetPathfindingRange(key)).VisualizeAsRange();
            graph.GetVertices(history.GetObstacles(key)).VisualizeAsObstacles();
        }
    }
}
