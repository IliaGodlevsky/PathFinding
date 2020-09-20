using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLibrary.Extensions.CustomTypeExtensions
{
    public static class IPathFindAlgorithmExtension
    {
        public static IEnumerable<IVertex> GetPath(this IPathFindingAlgorithm algorithm)
        {
            return algorithm.Graph.End.GetPathToStartVertex();
        }
    }
}
