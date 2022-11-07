using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Interface
{
    public interface IAlgorithm<out TPath>
        where TPath : IEnumerable<ICoordinate>
    {
        TPath FindPath();
    }
}
