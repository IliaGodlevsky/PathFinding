using Algorithm.PathFindingAlgorithms.Interface;
using GraphLib.Extensions;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    public static class IPathFindAlgorithmExtension
    {
        public static IEnumerable<IVertex> GetPath(this IPathFindingAlgorithm self)
        {
            if (self.Graph.Start.IsVisited)
            {
                return self.Graph.End.GetPathToStartVertex();
            }

            return new DefaultVertex[] { };
        }
    }
}
