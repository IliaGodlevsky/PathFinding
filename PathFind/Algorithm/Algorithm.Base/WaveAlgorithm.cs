using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    /// <summary>
    /// A base class for all wave algorithms, such as 
    /// Dijkstra's algorithm, Lee algorithm or A* algorithm.
    /// This is an abstract class
    /// </summary>
    [NotLoadable]
    public abstract class WaveAlgorithm : Algorithm
    {
        protected WaveAlgorithm(IGraph graph, IEndPoints endPoints)
            : base(graph, endPoints)
        {
            verticesQueue = new Queue<IVertex>();
        }

        protected abstract IVertex NextVertex { get; }

        public override sealed IGraphPath FindPath()
        {
            PrepareForPathfinding();
            do
            {
                var neighbours = GetCurrentVertexUnvisitedNeighbours();
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
            return new GraphPath(parentVertices, endPoints);
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

        private IVertex[] GetCurrentVertexUnvisitedNeighbours()
        {
            return visitedVertices
                .GetUnvisitedNeighbours(CurrentVertex)
                .FilterObstacles()
                .ToArray();
        }
    }
}