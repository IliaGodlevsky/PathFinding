using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex;

namespace GraphLibrary.Extensions.CustomTypeExtensions
{
    public static class IGraphExtensions
    {
        public static string GetFormattedInfo(this IGraph graph, string format)
        {
            return string.Format(format, graph.Width, graph.Height,
               graph.ObstaclePercent, graph.ObstacleNumber, graph.Size);
        }

        public static void Refresh(this IGraph graph)
        {
            graph.RemoveExtremeVertices();
            graph.Array.Apply(vertex => vertex.Refresh());
        }

        public static void RemoveExtremeVertices(this IGraph graph)
        {
            graph.End = NullVertex.Instance;
            graph.Start = NullVertex.Instance;
        }
    }
}
