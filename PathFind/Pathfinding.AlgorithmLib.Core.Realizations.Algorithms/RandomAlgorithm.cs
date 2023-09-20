using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Random;
using Shared.Random.Extensions;
using Shared.Random.Realizations;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    public sealed class RandomAlgorithm : BreadthFirstAlgorithm<List<IVertex>>
    {
        private readonly IRandom random;

        public RandomAlgorithm(IEnumerable<IVertex> pathfindingRange, IRandom random)
            : base(pathfindingRange)
        {
            this.random = random;
        }

        public RandomAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new PseudoRandom())
        {

        }

        protected override void DropState()
        {
            base.DropState();
            storage.Clear();
        }

        protected override IVertex GetNextVertex()
        {
            if (storage.Count > 0)
            {
                int limit = storage.Count - 1;
                int index = random.NextInt(limit);
                var vertex = storage[index];
                storage.RemoveAt(index);
                return vertex;
            }
            throw new DeadendVertexException();
        }

        protected override void RelaxVertex(IVertex vertex)
        {
            storage.Add(vertex);
            base.RelaxVertex(vertex);
        }

        public override string ToString()
        {
            return Languages.RandomAlgorithm;
        }
    }
}
