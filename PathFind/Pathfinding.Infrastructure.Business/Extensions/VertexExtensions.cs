using Pathfinding.Domain.Interface;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class VertexExtensions
    {
        public static bool IsNeighbor(this IVertex self, IVertex candidate)
        {
            return self.Neighbours.Contains(candidate);
        }
    }
}
