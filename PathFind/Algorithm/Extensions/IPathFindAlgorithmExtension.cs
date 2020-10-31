using Algorithm.PathFindingAlgorithms.Interface;
using GraphLib.Extensions;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    public static class IPathFindAlgorithmExtension
    {
        public static IEnumerable<IVertex> GetPath(this IPathFindingAlgorithm algorithm)
        {
            return algorithm.Graph.End.GetPathToStartVertex();
        }
    }
}
