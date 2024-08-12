using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Algorithms;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface
{
    public interface IAlgorithmFactory<out TAlgorithm>
        where TAlgorithm : IAlgorithm<IEnumerable<Coordinate>>
    {
        TAlgorithm Create(IEnumerable<IVertex> pathfindingRange);
    }
}