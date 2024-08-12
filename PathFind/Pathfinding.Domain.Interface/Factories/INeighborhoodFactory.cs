using Pathfinding.Shared.Primitives;

namespace Pathfinding.Domain.Interface.Factories
{
    public interface INeighborhoodFactory
    {
        INeighborhood CreateNeighborhood(Coordinate coordinate);
    }
}
