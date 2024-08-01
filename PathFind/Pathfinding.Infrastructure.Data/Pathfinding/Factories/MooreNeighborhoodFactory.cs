using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;

namespace Pathfinding.Infrastructure.Data.Pathfinding.Factories
{
    public sealed class MooreNeighborhoodFactory : INeighborhoodFactory
    {
        public INeighborhood CreateNeighborhood(ICoordinate coordinate)
        {
            return new MooreNeighborhood(coordinate);
        }
    }
}