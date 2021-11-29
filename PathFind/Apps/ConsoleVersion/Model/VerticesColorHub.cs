using Common.Extensions;
using System.Drawing;

namespace ConsoleVersion.Model
{
    internal sealed class VerticesColorHub
    {
        private static readonly Color RegularVertexColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
        private static readonly Color ObstacleVertexColor = Color.FromKnownColor(KnownColor.Black);
        private static readonly Color PathVertexColor = Color.FromKnownColor(KnownColor.Yellow);
        private static readonly Color EnqueuedVertexColor = Color.FromKnownColor(KnownColor.Magenta);
        private static readonly Color SourceVertexColor = Color.FromKnownColor(KnownColor.Green);
        private static readonly Color TargetVertexColor = Color.FromKnownColor(KnownColor.Red);
        private static readonly Color AlreadyPathVertexColor = Color.FromKnownColor(KnownColor.Orange);
        private static readonly Color VisitedVertexColor = Color.FromKnownColor(KnownColor.Blue);
        private static readonly Color IntermediateVertexColor = Color.FromKnownColor(KnownColor.DarkOrange);
        private static readonly Color ToReplaceMarkColor = Color.FromKnownColor(KnownColor.DarkRed);

        private void Mark(Vertex vertex, Color color)
        {
            vertex.Color = color;
            vertex.Display();
        }

        public bool IsVisualizedAsPath(Vertex vertex) => vertex.Color.IsOneOf(PathVertexColor, AlreadyPathVertexColor, IntermediateVertexColor);
        public bool IsVisualizedAsEndPoint(Vertex vertex) => vertex.Color.IsOneOf(SourceVertexColor, TargetVertexColor, IntermediateVertexColor, ToReplaceMarkColor);
        public void VisualizeAsTarget(Vertex vertex) => Mark(vertex, TargetVertexColor);
        public void VisualizeAsObstacle(Vertex vertex) => Mark(vertex, ObstacleVertexColor);
        public void VisualizeAsSource(Vertex vertex) => Mark(vertex, SourceVertexColor);
        public void VisualizeAsIntermediate(Vertex vertex) => Mark(vertex, IntermediateVertexColor);

        public void VisualizeAsRegular(Vertex vertex)
        {
            Mark(vertex, RegularVertexColor);
        }

        public void VisualizeAsPath(Vertex vertex)
        {
            if (vertex.IsVisualizedAsPath)
            {
                Mark(vertex, AlreadyPathVertexColor);
            }
            else
            {
                Mark(vertex, PathVertexColor);
            }
        }



        public void VisualizeAsVisited(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPath)
            {
                Mark(vertex, VisitedVertexColor);
            }
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPath)
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
    }
}
