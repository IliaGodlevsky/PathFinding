using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Factories
{
    public sealed class LeeAlgorithmFactory : IAlgorithmFactory<LeeAlgorithm>
    {
        public LeeAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new LeeAlgorithm(pathfindingRange);
        }
    }
}
