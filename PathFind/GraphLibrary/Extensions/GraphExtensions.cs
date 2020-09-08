using GraphLibrary.Extensions.CollectionExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.Extensions
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
            graph.RemoveExtremeVertices();
            graph.Array.Apply(RefreshVertex);
        }

        public static void RemoveExtremeVertices(this Graph graph)
        {
            graph.End = NullVertex.GetInstance();
            graph.Start = NullVertex.GetInstance();
        }
    }
}
