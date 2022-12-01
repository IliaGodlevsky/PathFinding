using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Factory.Interface
{
    public interface IAlgorithmFactory<out TAlgorithm>
        where TAlgorithm : IAlgorithm<IEnumerable<ICoordinate>>
    {
        TAlgorithm Create(IEnumerable<IVertex> pathfindingRange);
    }
}