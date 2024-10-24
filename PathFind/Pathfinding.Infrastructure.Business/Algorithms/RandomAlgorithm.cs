using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Interface;
using Pathfinding.Shared.Random;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
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
            : this(pathfindingRange, new XorshiftRandom())
        {

        }

        protected override void DropState()
        {
            base.DropState();
            storage.Clear();
        }

        protected override void MoveNextVertex()
        {
            if (storage.Count == 0)
            {
                throw new DeadendVertexException();
            }
            int limit = storage.Count - 1;
            int index = random.NextInt(limit);
            var vertex = storage[index];
            storage.RemoveAt(index);
            CurrentVertex = vertex;
        }

        protected override void RelaxVertex(IVertex vertex)
        {
            storage.Add(vertex);
            base.RelaxVertex(vertex);
        }
    }
}
