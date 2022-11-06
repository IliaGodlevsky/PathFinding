using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface INeighborhoodFactory
    {
        INeighborhood CreateNeighborhood(ICoordinate coordinate);
    }
}