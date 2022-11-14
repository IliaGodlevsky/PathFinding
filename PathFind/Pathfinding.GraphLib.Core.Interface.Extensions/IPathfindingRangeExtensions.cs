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

        public static IEnumerable<IVertex> GetIntermediates(this IPathfindingRange self)
        {
            var vertices = new[] { self.Source, self.Target };
            return self.Where(item => !vertices.Contains(item));
        }
    }
}
