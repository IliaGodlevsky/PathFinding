using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface.Algorithms
{
    public interface IAlgorithm<out TPath>
        where TPath : IEnumerable<ICoordinate>
    {
        TPath FindPath();
    }
}
