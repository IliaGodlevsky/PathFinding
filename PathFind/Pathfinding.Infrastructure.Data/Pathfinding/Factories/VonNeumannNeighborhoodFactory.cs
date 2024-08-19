using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Data.Pathfinding.Factories
{
    public sealed class VonNeumannNeighborhoodFactory : INeighborhoodFactory
    {
        public INeighborhood CreateNeighborhood(Coordinate coordinate)
        {
            return new VonNeumannNeighborhood(coordinate);
        }
    }
}