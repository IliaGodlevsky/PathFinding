using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Interface;
using Pathfinding.Shared.Random;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Factories
{
    public sealed class RandomAlgorithmFactory : IAlgorithmFactory<RandomAlgorithm>
    {
        private readonly IRandom random;

        public RandomAlgorithmFactory(IRandom random)
        {
            this.random = random;
        }

        public RandomAlgorithmFactory()
            : this(new CongruentialRandom())
        {

        }

        public RandomAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new RandomAlgorithm(pathfindingRange, random);
        }
    }
}
