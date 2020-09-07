using GraphLibrary.Algorithm;
using GraphLibrary.Common.Extensions.CollectionExtensions;
using GraphLibrary.Extensions;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Common.Extensions
{
    public static class IPathFindAlgorithmExtension
    {
        public static void DrawPath(this IPathFindAlgorithm algo)
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

        public static IEnumerable<IVertex> GetFoundPath(this IPathFindAlgorithm algorithm)
        {
            var vert = algorithm.Graph?.End;
            while (vert?.ParentVertex != null)
            {
                vert = vert.ParentVertex;
                if (vert.IsSimpleVertex())
                    yield return vert;
            }
        }
    }
}
