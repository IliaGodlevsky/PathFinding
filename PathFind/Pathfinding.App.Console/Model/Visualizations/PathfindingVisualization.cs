using Pathfinding.VisualizationLib.Core.Interface;
using System;

namespace Pathfinding.App.Console.Model.Visualizations
{
    internal sealed class PathfindingVisualization : IPathfindingVisualization<Vertex>
    {
        public ConsoleColor EnqueuedVertexColor { get; set; } = ConsoleColor.Blue;

        public ConsoleColor VisitedVertexColor { get; set; } = ConsoleColor.White;

        public void VisualizeAsEnqueued(Vertex visualizable)
        {
            if (!visualizable.IsVisualizedAsRange() && !visualizable.IsVisualizedAsPath())
            {
                visualizable.Color = EnqueuedVertexColor;
            }
        }

        public void VisualizeAsVisited(Vertex visualizable)
        {
            if (!visualizable.IsVisualizedAsRange() && !visualizable.IsVisualizedAsPath())
            {
                visualizable.Color = VisitedVertexColor;
            }
        }
    }
}
