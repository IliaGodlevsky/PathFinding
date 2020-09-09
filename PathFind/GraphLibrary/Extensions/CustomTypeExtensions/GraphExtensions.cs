using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex;

namespace GraphLibrary.Extensions.CustomTypeExtensions
{
    public static class GraphExtensions
    {
        public static string GetFormattedInfo(this Graph graph, string format)
        {
            return string.Format(format, graph.Width, graph.Height,
               graph.ObstaclePercent, graph.ObstacleNumber, graph.Size);
        }

        public static void Refresh(this Graph graph)
        {
            graph.RemoveExtremeVertices();
            graph.Array.Apply(vertex => vertex.Refresh());
        }

        public static void RemoveExtremeVertices(this Graph graph)
        {
            graph.End = NullVertex.GetInstance();
            graph.Start = NullVertex.GetInstance();
        }
    }
}
