using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using Common.Extensions;
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

        public override IGraphPath FindPath()
        {
            PrepareForPathfinding();
            do
            {
                var neighbours 
                    = GetCurrentVertexNeighbours()
                        .ToArray();
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

        protected abstract void RelaxNeighbours(IEnumerable<IVertex> vertex);

        protected virtual void ExtractNeighbours(IEnumerable<IVertex> neighbours)
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

        private IEnumerable<IVertex> GetCurrentVertexNeighbours()
        {
            return visitedVertices.GetUnvisitedNeighbours(CurrentVertex);
        }
    }
}