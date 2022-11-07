using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Abstractions
{
    public abstract class WaveAlgorithm<TStorage> : PathfindingAlgorithm<TStorage>
        where TStorage : new()
    {
        protected WaveAlgorithm(IPathfindingRange endPoints)
            : base(endPoints)
        {
        }

        protected abstract void RelaxVertex(IVertex vertex);

        protected override void PrepareForSubPathfinding(Range range)
        {
            base.PrepareForSubPathfinding(range);
            VisitCurrentVertex();
        }

        protected override void VisitCurrentVertex()
        {
            visited.Add(CurrentVertex);
            RaiseVertexVisited(new PathfindingEventArgs(CurrentVertex));
        }

        protected virtual void RelaxNeighbours(IReadOnlyCollection<IVertex> vertices)
        {
            vertices.ForEach(RelaxVertex);
        }

        protected override void InspectVertex(IVertex vertex)
        {
            var neighbours = GetUnvisitedNeighbours(vertex);
            neighbours.ForEach(Enqueued);
            RelaxNeighbours(neighbours);
        }
    }
}