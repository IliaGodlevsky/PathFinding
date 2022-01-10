using GraphLib.Interfaces;

namespace Visualization.Extensions
{
    internal static class IVertexExtensions
    {
        public static bool TryVisualizeAsVisited(this IVertex vertex)
        {
            if (vertex is IVisualizable vert && !vert.IsVisualizedAsEndPoint)
            {
                vert.VisualizeAsVisited();
                return true;
            }

            return false;
        }

        public static bool TryVisualizeAsEnqueued(this IVertex vertex)
        {
            if (vertex is IVisualizable vert && !vert.IsVisualizedAsEndPoint)
            {
                vert.VisualizeAsEnqueued();
                return true;
            }

            return false;
        }
    }
}
