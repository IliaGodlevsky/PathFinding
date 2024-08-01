namespace Pathfinding.Domain.Interface.Factories
{
    public interface INeighborhoodFactory
    {
        INeighborhood CreateNeighborhood(ICoordinate coordinate);
    }
}
