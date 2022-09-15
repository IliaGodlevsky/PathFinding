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
        protected virtual IEndPoints CurrentEndPoints { get; set; }

        protected WaveAlgorithm(IEndPoints endPoints)
            : base(endPoints)
        {

        }

        public sealed override IGraphPath FindPath()
        {
            var path = NullGraphPath.Interface;
            PrepareForPathfinding();
            var subEndPoints = endPoints.ToSubEndPoints();
            using (var iterator = subEndPoints.GetEnumerator())
            {
                while (iterator.MoveNext() && !IsInterruptRequested)
                {
                    CurrentEndPoints = iterator.Current;
                    PrepareForLocalPathfinding();
                    VisitVertex(CurrentVertex);
                    while (!IsDestination(CurrentEndPoints))
                    {
                        WaitUntilResumed();
                        InspectVertex(CurrentVertex);
                        CurrentVertex = GetNextVertex();
                        VisitVertex(CurrentVertex);
                    }
                    path = new CompositeGraphPath(path, CreateGraphPath());
                    Reset();
                }
            }
            CompletePathfinding();
            return IsInterruptRequested ? NullGraphPath.Interface : path;
        }

        protected virtual void PrepareForLocalPathfinding()
        {
            CurrentVertex = CurrentEndPoints.Source;
        }

        protected virtual IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, CurrentEndPoints);
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