using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Attributes;
using Shared.Random;
using Shared.Random.Realizations;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Factory
{
    [Order(4)]
    [WaveGroup]
    public sealed class RandomAlgorithmFactory : IAlgorithmFactory<RandomAlgorithm>
    {
        private readonly IRandom random;

        public RandomAlgorithmFactory(IRandom random)
        {
            this.random = random;
        }

        public RandomAlgorithmFactory()
            : this(new PseudoRandom()) 
        { 

        }

        public RandomAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new RandomAlgorithm(pathfindingRange, random);
        }

        public override string ToString()
        {
            return Languages.RandomAlgorithm;
        }
    }
}
