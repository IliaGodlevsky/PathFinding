using Algorithm.Infrastructure.EventArguments;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Base
{
    public abstract class WaveAlgorithm : RangePathfindingAlgorithm
    {
        protected WaveAlgorithm(IEndPoints endPoints)
            : base(endPoints)
        {

        }       

        protected override void VisitVertex(IVertex vertex)
        {
            visitedVertices.Visit(vertex);
            RaiseVertexVisited(new AlgorithmEventArgs(vertex));
        }

        protected abstract void RelaxNeighbours(IReadOnlyCollection<IVertex> vertex);

        protected override void InspectVertex(IVertex vertex)
        {
            var neighbours = GetUnvisitedVertices(vertex);
            neighbours.ForEach(Enqueued);
            RelaxNeighbours(neighbours);
        }
    }
}