using Pathfinding.Domain.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class BidirectBreadthFirstAlgorithm<TStorage> : BidirectWaveAlgorithm<TStorage>
        where TStorage : new()
    {
        protected BidirectBreadthFirstAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        protected override void RelaxForwardVertex(IVertex vertex)
        {
            forwardVisited.Add(vertex);
            forwardTraces[vertex.Position] = Current.Forward;
        }

        protected override void RelaxBackwardVertex(IVertex vertex)
        {
            backwardVisited.Add(vertex);
            backwardTraces[vertex.Position] = Current.Backward;
        }
    }
}
