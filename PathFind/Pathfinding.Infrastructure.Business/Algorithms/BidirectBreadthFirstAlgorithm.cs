using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class BidirectBreadthFirstAlgorithm<TStorage> : BidirectWaveAlgorithm<TStorage>
        where TStorage : new()
    {
        protected BidirectBreadthFirstAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        protected override void RelaxForwardVertex(IPathfindingVertex vertex)
        {
            forwardVisited.Add(vertex);
            forwardTraces[vertex.Position] = Current.Forward;
        }

        protected override void RelaxBackwardVertex(IPathfindingVertex vertex)
        {
            backwardVisited.Add(vertex);
            backwardTraces[vertex.Position] = Current.Backward;
        }
    }
}
