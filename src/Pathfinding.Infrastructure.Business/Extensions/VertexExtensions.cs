using Pathfinding.Service.Interface;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class VertexExtensions
    {
        public static bool IsNeighbor(this IPathfindingVertex self, IPathfindingVertex candidate)
        {
            return self.Neighbors.Contains(candidate);
        }
    }
}
