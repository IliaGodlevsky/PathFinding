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
    }
}
