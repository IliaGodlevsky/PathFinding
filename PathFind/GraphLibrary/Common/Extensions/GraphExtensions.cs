using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System.Linq;

namespace GraphLibrary.Common.Extensions
{
    public static class GraphExtensions
    {
        public static string GetFormattedInfo(this Graph graph, string format)
        {
            return string.Format(format, graph.Width, graph.Height,
               graph.ObstaclePercent, graph.ObstacleNumber, graph.Size);
        }

        public static int GetNumberOfVisitedVertices(this Graph graph)
        {
            return graph.GetArray().Cast<IVertex>().Count(vertex => vertex.IsVisited);
        }
    }
}
