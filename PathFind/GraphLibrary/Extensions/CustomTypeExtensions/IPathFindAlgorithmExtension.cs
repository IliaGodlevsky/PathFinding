using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Extensions.CustomTypeExtensions
{
    public static class IPathFindAlgorithmExtension
    {
        public static void DrawPath(this IPathFindingAlgorithm algo)
        {
            var path = algo.GetFoundPath().ToList();
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
            if (algorithm.Graph.End.IsVisited)
            {
                var vertex = algorithm.Graph.End;
                while (algorithm.Graph.Start != vertex
                    && vertex != NullVertex.Instance)
                {
                    if (!vertex.IsStart)
                        yield return vertex;
                    vertex = vertex.ParentVertex;
                }
            }
        }
    }
}
