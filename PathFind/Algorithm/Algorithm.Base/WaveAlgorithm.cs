using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Algorithm.Realizations.GraphPaths;
using Common.Disposables;
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
            PrepareForPathfinding();
            using (Disposable.Use(CompletePathfinding))
            {
                var path = NullGraphPath.Interface;
                foreach (var subEndPoint in endPoints.ToSubEndPoints())
                {
                    CurrentEndPoints = subEndPoint;
                    PrepareForLocalPathfinding();
                    VisitVertex(CurrentVertex);
                    while (!IsDestination(CurrentEndPoints))
                    {
                        ThrowIfInterrupted();
                        WaitUntilResumed();
                        InspectVertex(CurrentVertex);
                        CurrentVertex = GetNextVertex();
                        VisitVertex(CurrentVertex);
                    }
                    var subPath = CreateGraphPath();
                    path = new CompositeGraphPath(path, subPath);
                    Reset();
                }
                return path;
            }
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