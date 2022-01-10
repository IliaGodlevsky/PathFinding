using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Algorithm.Realizations.GraphPaths;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using Interruptable.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Algorithm.Base
{
    /// <summary>
    /// A base class for all wave algorithms, such as Dijkstra's algorithm,
    /// A* algorithm or Lee algorithm. This class is abstract
    /// </summary>
    public abstract class WaveAlgorithm : PathfindingAlgorithm, IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        protected WaveAlgorithm(IEndPoints endPoints)
            : base(endPoints)
        {

        }

        public override sealed IGraphPath FindPath()
        {
            IGraphPath path = NullGraphPath.Instance;
            PrepareForPathfinding();
            foreach (var endPoint in endPoints.ToSubEndPoints())
            {
                CurrentEndPoints = endPoint;
                PrepareForLocalPathfinding();
                VisitVertex(CurrentVertex);
                while (!IsDestination(CurrentEndPoints))
                {
                    InspectCurrentVertex();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InspectCurrentVertex()
        {
            var neighbours = visitedVertices
                .GetUnvisitedNeighbours(CurrentVertex)
                .FilterObstacles()
                .ToArray();
            neighbours.ForEach(RaiseVertexEnqueued);
            RelaxNeighbours(neighbours);
        }
    }
}