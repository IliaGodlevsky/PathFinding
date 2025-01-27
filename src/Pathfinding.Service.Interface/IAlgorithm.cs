using Pathfinding.Shared.Primitives;

namespace Pathfinding.Service.Interface.Algorithms
{
    public interface IAlgorithm<out TPath>
        where TPath : IEnumerable<Coordinate>
    {
        TPath FindPath();
    }
}
