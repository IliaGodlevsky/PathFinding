using GraphLibrary.Collection;

namespace GraphLibrary.Common.Extensions
{
    public static class GraphExtensions
    {
        public static string GetFormattedInfo(this Graph graph, string format)
        {
            return string.Format(format, graph.Width, graph.Height,
               graph.ObstaclePercent, graph.ObstacleNumber, graph.Size);
        }
    }
}
