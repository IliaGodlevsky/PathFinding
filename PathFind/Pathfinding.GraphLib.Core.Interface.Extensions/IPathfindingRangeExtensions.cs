using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class IPathfindingRangeExtensions
    {
        public static bool IsInRange(this IPathfindingRange self, IVertex vertex)
        {
            return self.Contains(vertex);
        }

        public static bool CanBeInRange(this IPathfindingRange range, IVertex vertex)
        {
            return !vertex.IsIsolated() && !range.IsInRange(vertex);
        }

        public static bool IsIntermediate(this IPathfindingRange range, IVertex vertex)
        {
            var sourceAndTarget = new[] { range.Source, range.Target };
            return range.Except(sourceAndTarget).Contains(vertex);
        }

        public static bool HasSourceAndTargetSet(this IPathfindingRange range)
        {
            return range.Source?.IsIsolated() == false && range.Target?.IsIsolated() == false;
        }

        public static bool HasIsolators(this IPathfindingRange range)
        {
            return range.Any(vertex => vertex.IsIsolated());
        }
    }
}
