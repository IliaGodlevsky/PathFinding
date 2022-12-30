using Pathfinding.AlgorithmLib.History;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Linq;

namespace Pathfinding.Visualization.Extensions
{
    public static class HistoryExtensions
    {
        public static void VisualizeHistory<TVertex, TVolume>(this History<TVolume> history, Guid key, IGraph<TVertex> graph)
            where TVertex : IVertex, IVisualizable
            where TVolume : IHistoryVolume<ICoordinate>, new()
        {
            history.GetRegulars(key).Select(graph.Get).ForEach(vertex => vertex.VisualizeAsRegular());
            history.GetObstacles(key).Select(graph.Get).VisualizeAsObstacles();
            history.GetVisitedVertices(key).Select(graph.Get).VisualizeAsVisited();
            history.GetPathfindingRange(key).Select(graph.Get).VisualizeAsRange();
            history.GetPath(key).Select(graph.Get).VisualizeAsPath();
        }
    }
}
