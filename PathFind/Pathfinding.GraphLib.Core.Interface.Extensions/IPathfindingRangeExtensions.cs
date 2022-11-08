using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class IPathfindingRangeExtensions
    {
        public static bool IsEndPoint(this IPathfindingRange self, IVertex vertex)
        {
            return self.Contains(vertex);
        }

        public static bool IsIntermediate(this IPathfindingRange self, IVertex vertex)
        {
            return self.GetIntermediates().Any(v => v.IsEqual(vertex));
        }

        public static bool CanBeEndPoint(this IPathfindingRange self, IVertex vertex)
        {
            return !vertex.IsIsolated() && !self.IsEndPoint(vertex);
        }

        public static bool HasSourceAndTargetSet(this IPathfindingRange self)
        {
            return !self.Source.IsIsolated() && !self.Target.IsIsolated();
        }

        public static bool HasIsolators(this IPathfindingRange self)
        {
            return self.Any(vertex => vertex.IsIsolated());
        }

        public static IEnumerable<IVertex> GetIntermediates(this IPathfindingRange self)
        {
            var vertices = new[] { self.Source, self.Target };
            return self.Where(item => !vertices.Contains(item));
        }
    }
}
