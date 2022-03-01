using Common.Extensions;
using GraphLib.Interfaces;
using System.Drawing;

namespace WindowsFormsVersion.Model
{
    internal sealed class VertexVisualization : IVisualization<Vertex>
    {
        private static readonly Color RegularVertexColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
        private static readonly Color ObstacleVertexColor = Color.FromKnownColor(KnownColor.Black);
        private static readonly Color PathVertexColor = Color.FromKnownColor(KnownColor.Yellow);
        private static readonly Color EnqueuedVertexColor = Color.FromKnownColor(KnownColor.Magenta);
        private static readonly Color SourceVertexColor = Color.FromKnownColor(KnownColor.Green);
        private static readonly Color TargetVertexColor = Color.FromKnownColor(KnownColor.Red);
        private static readonly Color AlreadyPathVertexColor = Color.FromKnownColor(KnownColor.Gold);
        private static readonly Color VisitedVertexColor = Color.FromKnownColor(KnownColor.CadetBlue);
        private static readonly Color IntermediateVertexColor = Color.FromKnownColor(KnownColor.DarkOrange);
        private static readonly Color ToReplaceMarkColor = Color.FromArgb(alpha: 185, red: 255, green: 140, blue: 0);

        public bool IsVisualizedAsPath(Vertex vertex) => vertex.BackColor.IsOneOf(PathVertexColor, AlreadyPathVertexColor, IntermediateVertexColor);
        public bool IsVisualizedAsEndPoint(Vertex vertex) => vertex.BackColor.IsOneOf(SourceVertexColor, TargetVertexColor, IntermediateVertexColor, ToReplaceMarkColor);
        public void VisualizeAsTarget(Vertex vertex) => vertex.BackColor = TargetVertexColor;
        public void VisualizeAsObstacle(Vertex vertex) => vertex.BackColor = ObstacleVertexColor;
        public void VisualizeAsSource(Vertex vertex) => vertex.BackColor = SourceVertexColor;
        public void VisualizeAsIntermediate(Vertex vertex) => vertex.BackColor = IntermediateVertexColor;
        public void VisualizeAsRegular(Vertex vertex) => vertex.BackColor = RegularVertexColor;

        public void VisualizeAsPath(Vertex vertex)
        {
            if (vertex.IsVisualizedAsPath)
            {
                vertex.BackColor = AlreadyPathVertexColor;
            }
            else
            {
                vertex.BackColor = PathVertexColor;
            }
        }

        public void VisualizeAsVisited(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPath)
            {
                vertex.BackColor = VisitedVertexColor;
            }
        }

        public void VisualizeAsEnqueued(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsPath)
            {
                vertex.BackColor = EnqueuedVertexColor;
            }
        }

        public void VisualizeAsMarkedToReplaceIntermediate(Vertex vertex)
        {
            if (vertex.BackColor == IntermediateVertexColor)
            {
                vertex.BackColor = ToReplaceMarkColor;
            }
        }
    }
}
