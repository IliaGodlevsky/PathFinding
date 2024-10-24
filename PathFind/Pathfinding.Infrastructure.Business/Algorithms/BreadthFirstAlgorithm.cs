using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class BreadthFirstAlgorithm<TStorage> : WaveAlgorithm<TStorage>
        where TStorage : new()
    {
        public BreadthFirstAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        protected override void RelaxVertex(IVertex vertex)
        {
            visited.Add(vertex);
            traces[vertex.Position] = CurrentVertex;
        }
    }
}
