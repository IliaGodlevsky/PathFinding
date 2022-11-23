using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Drawing;

namespace Pathfinding.App.Console.Model
{
    internal sealed class VertexVisualization : IVisualization<Vertex>
    {   
        private static readonly Color RegularVertexColor = Color.White;
        private static readonly Color ObstacleVertexColor = Color.Black;
        private static readonly Color PathVertexColor = Color.Yellow;
        private static readonly Color EnqueuedVertexColor = Color.Magenta;
        private static readonly Color SourceVertexColor = Color.Green;
        private static readonly Color TargetVertexColor = Color.Red;
        private static readonly Color AlreadyPathVertexColor = Color.Orange;
        private static readonly Color VisitedVertexColor = Color.Blue;
        private static readonly Color IntermediateVertexColor = Color.DarkOrange;
        private static readonly Color ToReplaceMarkColor = Color.DarkRed;
        
        public bool IsVisualizedAsPath(Vertex vertex)
        {
            return vertex.Color.IsOneOf(PathVertexColor, AlreadyPathVertexColor, IntermediateVertexColor);
        }

        public bool IsVisualizedAsPathfindingRange(Vertex vertex)
        {
            return vertex.Color.IsOneOf(SourceVertexColor, TargetVertexColor, IntermediateVertexColor, ToReplaceMarkColor);
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

        public void VisualizeAsIntermediate(Vertex vertex)
        {
            Mark(vertex, IntermediateVertexColor);
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
            if (vertex.Color == IntermediateVertexColor)
            {
                Mark(vertex, ToReplaceMarkColor);
            }
        }

        private void Mark(Vertex vertex, Color color)
        {
            vertex.Color = color;
            vertex.Display();
        }
    }
}