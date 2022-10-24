using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Algorithm.Realizations.GraphPaths;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Base
{
    public abstract class WaveAlgorithm : PathfindingAlgorithm
    {
        protected virtual IPathfindingRange CurrentRange { get; set; }

        protected WaveAlgorithm(IPathfindingRange endPoints)
            : base(endPoints)
        {

        }

        public sealed override IGraphPath FindPath()
        {
            var path = NullGraphPath.Interface;
            PrepareForPathfinding();
            var subRanges = pathfindingRange.ToSubRanges().ToReadOnly();
            for (int i = 0; i < subRanges.Count && !IsInterruptRequested; i++)
            {
                CurrentRange = subRanges[i];
                PrepareForLocalPathfinding();
                VisitVertex(CurrentVertex);
                while (!IsDestination(CurrentRange))
                {
                    WaitUntilResumed();
                    InspectVertex(CurrentVertex);
                    CurrentVertex = GetNextVertex();
                    VisitVertex(CurrentVertex);
                }
                path = new CompositeGraphPath(path, CreateGraphPath());
                Reset();
            }
            CompletePathfinding();
            return IsInterruptRequested ? NullGraphPath.Interface : path;
        }

        protected virtual void PrepareForLocalPathfinding()
        {
            CurrentVertex = CurrentRange.Source;
        }

        protected virtual IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, CurrentRange);
        }

        protected virtual void VisitVertex(IVertex vertex)
        {
            ThrowIfDeadEnd(vertex);
            visitedVertices.Visit(vertex);
            RaiseVertexVisited(new AlgorithmEventArgs(vertex));
        }

        protected abstract void RelaxNeighbours(IReadOnlyCollection<IVertex> vertex);

        protected virtual void RaiseVertexEnqueued(IVertex vertex)
        {
            RaiseVertexEnqueued(new AlgorithmEventArgs(vertex));
        }

        private void InspectVertex(IVertex vertex)
        {
            var neighbours = GetUnvisitedVertices(vertex);
            neighbours.ForEach(RaiseVertexEnqueued);
            RelaxNeighbours(neighbours);
        }
    }
}