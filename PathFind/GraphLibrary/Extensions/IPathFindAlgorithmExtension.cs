using GraphLibrary.Extensions.CollectionExtensions;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Extensions
{
    public static class IPathFindAlgorithmExtension
    {
        public static void DrawPath(this IPathFindingAlgorithm algo)
        {
            var path = algo.GetFoundPath()?.ToList();
            if (path != null)
                path.Apply(vertex =>
                {
                    if (vertex.IsSimpleVertex())
                        vertex.MarkAsPath();
                    return vertex;
                });
        }

        public static IEnumerable<IVertex> GetFoundPath(this IPathFindingAlgorithm algorithm)
        {
            var vert = algorithm.Graph.End;
            while (vert.ParentVertex != NullVertex.GetInstance())
            {
                vert = vert.ParentVertex;
                if (vert.IsSimpleVertex())
                    yield return vert;
            }
        }
    }
}
