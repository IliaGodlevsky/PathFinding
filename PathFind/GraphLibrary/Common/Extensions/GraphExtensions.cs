using GraphLibrary.Collection;
using GraphLibrary.Extensions;
using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Vertex;

namespace GraphLibrary.Common.Extensions
{
    public static class GraphExtensions
    {
        private static IVertex RefreshVertex(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
            return vertex;
        }

        public static string GetFormattedInfo(this Graph graph, string format)
        {
            return string.Format(format, graph.Width, graph.Height,
               graph.ObstaclePercent, graph.ObstacleNumber, graph.Size);
        }

        public static void Refresh(this Graph graph)
        {
            graph.End = null;
            graph.Start = null;
            graph.GetArray().Apply(RefreshVertex);
        }
    }
}
