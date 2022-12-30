using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Abstractions
{
    internal abstract class WaveAlgorithm<TStorage> : PathfindingAlgorithm<TStorage>
        where TStorage : new()
    {
        protected WaveAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        protected abstract void RelaxVertex(IVertex vertex);

        protected override void PrepareForSubPathfinding(SubRange range)
        {
            base.PrepareForSubPathfinding(range);
            VisitCurrentVertex();
        }

        protected override void VisitCurrentVertex()
        {
            visited.Add(CurrentVertex);
            RaiseVertexVisited(CurrentVertex);
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