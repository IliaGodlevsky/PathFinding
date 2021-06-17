using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    public abstract class BaseWaveAlgorithm : BaseAlgorithm
    {
        protected BaseWaveAlgorithm(IGraph graph, IEndPoints endPoints)
            : base(graph, endPoints)
        {
            verticesQueue = new Queue<IVertex>();
        }

        protected abstract IVertex NextVertex { get; }

        public override IGraphPath FindPath()
        {
            PrepareForPathfinding();
            do
            {
                var neighbours = GetCurrentVertexNeighbours();
                ExtractNeighbours(neighbours);
                RelaxNeighbours(neighbours);
                CurrentVertex = NextVertex;
                VisitVertex(CurrentVertex);
            } while (!IsDestination());
            CompletePathfinding();

            return CreateGraphPath();
        }

        protected virtual IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, endPoints, graph);
        }

        protected virtual void VisitVertex(IVertex vertex)
        {
            visitedVertices.Add(vertex);
            var args = CreateEventArgs(vertex);
            RaiseOnVertexVisitedEvent(args);
        }

        protected abstract void RelaxNeighbours(IVertex[] vertex);

        protected virtual void ExtractNeighbours(IVertex[] neighbours)
        {
            foreach (var neighbour in neighbours)
            {
                var args = CreateEventArgs(neighbour);
                RaiseOnVertexEnqueuedEvent(args);
                verticesQueue.Enqueue(neighbour);
            }

            verticesQueue = verticesQueue
                .DistinctBy(Position)
                .ToQueue();
        }

        protected Queue<IVertex> verticesQueue;

        private IVertex[] GetCurrentVertexNeighbours()
        {
            return visitedVertices
                .GetUnvisitedNeighbours(CurrentVertex)
                .GetNotObstacles()
                .ToArray();
        }
    }
}