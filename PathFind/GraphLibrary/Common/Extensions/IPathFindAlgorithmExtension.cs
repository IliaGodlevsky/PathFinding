using GraphLibrary.Algorithm;
using GraphLibrary.Common.Extensions.CollectionExtensions;
using GraphLibrary.Extensions;
using GraphLibrary.Vertex;
using System.Linq;

namespace GraphLibrary.Common.Extensions
{
    public static class IPathFindAlgorithmExtension
    {
        private static IVertex ColorizeVertext(IVertex vertex)
        {
            if (vertex.IsSimpleVertex())
                vertex.MarkAsPath();
            return vertex;
        }

        public static void DrawPath(this IPathFindAlgorithm algo)
        {
            var path = algo.Graph.End.GetPathToStartVertex()?.ToList();
            if (path != null)
                path.Apply(ColorizeVertext);
        }

        public static void VisitVertex(this IPathFindAlgorithm algorithm, IVertex vertex)
        {
            vertex.IsVisited = true;
            if (vertex.IsSimpleVertex())
                vertex.MarkAsVisited();
        }

        public static bool HasFoundPathToEndVertex(this IPathFindAlgorithm algorithm)
        {
            return algorithm.Graph?.End?.IsVisited == true;
        }
    }
}
