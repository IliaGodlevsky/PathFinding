using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Algorithms;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface
{
    public interface IAlgorithmFactory<out TAlgorithm>
        where TAlgorithm : IAlgorithm<IEnumerable<ICoordinate>>
    {
        TAlgorithm Create(IEnumerable<IVertex> pathfindingRange);
    }
}