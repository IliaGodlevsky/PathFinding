using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Attributes;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Factory
{
    [Order(5)]
    [WaveGroup]
    public sealed class LeeAlgorithmFactory : IAlgorithmFactory<LeeAlgorithm>
    {
        public LeeAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new LeeAlgorithm(pathfindingRange);
        }

        public override string ToString()
        {
            return Languages.LeeAlgorithm;
        }
    }
}
