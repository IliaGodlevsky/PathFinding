using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Algorithm.Realizations.GraphPaths;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    public abstract class WaveAlgorithm : PathfindingAlgorithm
    {
        protected WaveAlgorithm(IEndPoints endPoints)
            : base(endPoints)
        {

        }

        public override sealed IGraphPath FindPath()
        {
            var path = NullGraphPath.Instance;
            PrepareForPathfinding();
            foreach (var endPoint in endPoints.ToSubEndPoints())
            {
                CurrentEndPoints = endPoint;
                PrepareForLocalPathfinding();
                VisitVertex(CurrentVertex);
                while (!IsDestination(CurrentEndPoints))
                {
                    WaitUntillResumed();
                    InspectVertex(CurrentVertex);
                    CurrentVertex = NextVertex;
                    VisitVertex(CurrentVertex);
                }
                if (!IsTerminatedPrematurely) break;
                var found = CreateGraphPath();
                path = new CombinedGraphPath(path, found);
                Reset();
            }
            CompletePathfinding();
            return !IsTerminatedPrematurely ? NullGraphPath.Instance : path;
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
            visitedVertices.Add(vertex);
            RaiseVertexVisited(new AlgorithmEventArgs(vertex));
        }

        protected abstract void RelaxNeighbours(IReadOnlyCollection<IVertex> vertex);
        protected virtual IEndPoints CurrentEndPoints { get; set; }

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