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
        private readonly ConsoleColor AlreadyPathVertexColor = ConsoleColor.DarkRed;
        private readonly ConsoleColor VisitedVertexColor = ConsoleColor.White;
        private readonly ConsoleColor TransitVertexColor = ConsoleColor.Green;
        private readonly ConsoleColor ToReplaceMarkColor = ConsoleColor.DarkMagenta;

        public bool IsVisualizedAsPath(Vertex vertex)
        {
            return vertex.Color.IsOneOf(PathVertexColor, AlreadyPathVertexColor, TransitVertexColor);
        }

        public bool IsVisualizedAsPathfindingRange(Vertex vertex)
        {
            return vertex.Color.IsOneOf(SourceVertexColor, TargetVertexColor, TransitVertexColor, ToReplaceMarkColor);
        }

        public void VisualizeAsTarget(Vertex vertex)
        {
            Mark(vertex, TargetVertexColor);
        }

        public void VisualizeAsObstacle(Vertex vertex)
        {
            Mark(vertex, ObstacleVertexColor);
        }

        public void VisualizeAsSource(Vertex vertex)
        {
            Mark(vertex, SourceVertexColor);
        }

        public void VisualizeAsTransit(Vertex vertex)
        {
            Mark(vertex, TransitVertexColor);
        }

        public void VisualizeAsRegular(Vertex vertex)
        {
            Mark(vertex, RegularVertexColor);
        }

        public void VisualizeAsPath(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPathfindingRange())
            {
                if (vertex.IsVisualizedAsPath())
                {
                    Mark(vertex, AlreadyPathVertexColor);
                }
                else
                {
                    Mark(vertex, PathVertexColor);
                }
            }
        }

        public void VisualizeAsVisited(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPath() && !vertex.IsVisualizedAsPathfindingRange())
            {
                Mark(vertex, VisitedVertexColor);
            }
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPath() && !vertex.IsVisualizedAsPathfindingRange())
            {
                Mark(vertex, EnqueuedVertexColor);
            }
        }

        public void VisualizeAsMarkedToReplaceIntermediate(Vertex vertex)
        {
            if (vertex.Color == TransitVertexColor)
            {
                Mark(vertex, ToReplaceMarkColor);
            }
        }

        private void Mark(Vertex vertex, ConsoleColor color)
        {
            vertex.Color = color;
            vertex.Display();
        }
    }
}