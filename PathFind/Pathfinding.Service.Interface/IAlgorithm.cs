using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Algorithms
{
    public interface IAlgorithm<out TPath>
        where TPath : IEnumerable<Coordinate>
    {
        TPath FindPath();
    }
}
