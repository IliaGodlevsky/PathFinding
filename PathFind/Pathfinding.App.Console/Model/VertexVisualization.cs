using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;

namespace Pathfinding.App.Console.Model
{
    internal sealed class VertexVisualization : IVisualization<Vertex>
    {
        private readonly ConsoleColor RegularVertexColor = ConsoleColor.DarkGray;
        private readonly ConsoleColor ObstacleVertexColor = ConsoleColor.Black;
        private readonly ConsoleColor PathVertexColor = ConsoleColor.DarkYellow;
        private readonly ConsoleColor EnqueuedVertexColor = ConsoleColor.Blue;
        private readonly ConsoleColor SourceVertexColor = ConsoleColor.Magenta;
        private readonly ConsoleColor TargetVertexColor = ConsoleColor.Red;
        private readonly ConsoleColor CrossPathVertexColor = ConsoleColor.DarkRed;
        private readonly ConsoleColor VisitedVertexColor = ConsoleColor.White;
        private readonly ConsoleColor TransitVertexColor = ConsoleColor.Green;

        public bool IsVisualizedAsPath(Vertex vertex)
        {
            return vertex.Color.IsOneOf(PathVertexColor, CrossPathVertexColor);
        }

        public bool IsVisualizedAsPathfindingRange(Vertex vertex)
        {
            return vertex.Color.IsOneOf(SourceVertexColor, TargetVertexColor, TransitVertexColor);
        }

        public void VisualizeAsTarget(Vertex vertex) => vertex.Color = TargetVertexColor;

        public void VisualizeAsObstacle(Vertex vertex) => vertex.Color = ObstacleVertexColor;

        public void VisualizeAsSource(Vertex vertex) => vertex.Color = SourceVertexColor;

        public void VisualizeAsTransit(Vertex vertex) => vertex.Color = TransitVertexColor;

        public void VisualizeAsRegular(Vertex vertex) => vertex.Color = RegularVertexColor;

        public void VisualizeAsPath(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPathfindingRange())
            {
                if (vertex.IsVisualizedAsPath())
                {
                    vertex.Color = CrossPathVertexColor;
                }
                else
                {
                    vertex.Color = PathVertexColor;
                }
            }
        }

        public void VisualizeAsVisited(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPath() && !vertex.IsVisualizedAsPathfindingRange())
            {
                vertex.Color = VisitedVertexColor;
            }
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPath() && !vertex.IsVisualizedAsPathfindingRange())
            {
                vertex.Color = EnqueuedVertexColor;
            }
        }
    }
}