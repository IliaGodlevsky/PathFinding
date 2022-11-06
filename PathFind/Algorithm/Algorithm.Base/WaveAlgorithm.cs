using Algorithm.Infrastructure.EventArguments;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Base
{
    public abstract class WaveAlgorithm<TStorage> : PathfindingAlgorithm<TStorage>
        where TStorage : new()
    {
        protected WaveAlgorithm(IEndPoints endPoints)
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
            RaiseVertexVisited(new AlgorithmEventArgs(CurrentVertex));
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