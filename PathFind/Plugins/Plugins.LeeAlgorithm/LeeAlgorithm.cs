using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Algorithm.Base.CompanionClasses;
using Algorithm.Сompanions;

namespace Plugins.LeeAlgorithm
{
    [ClassName("Lee algorithm")]
    public class LeeAlgorithm : BaseAlgorithm
    {
        public LeeAlgorithm(IGraph graph, IEndPoints endPoints) 
            : base(graph, endPoints)
        {
            verticesQueue = new Queue<IVertex>();
        }

        public override IGraphPath FindPath()
        {
            PrepareForPathfinding();
            do
            {
                ExtractNeighbours();
                SpreadWaves();
                CurrentVertex = NextVertex;
                VisitVertex(CurrentVertex);
            } while (!IsDestination());
            CompletePathfinding();

            return new GraphPath(parentVertices, endPoints, graph);
        }

        protected override void PrepareForPathfinding()
        {
            base.PrepareForPathfinding();
            accumulatedCosts = new AccumulatedCosts(graph, 0);
        }

        protected virtual void VisitVertex(IVertex vertex)
        {
            visitedVertices.Add(vertex);
            var args = CreateEventArgs(vertex);
            RaiseOnVertexVisitedEvent(args);
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            verticesQueue.Clear();
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .Where(visitedVertices.IsNotVisited)
                    .ToQueue();
                return verticesQueue.DequeueOrDefault();
            }
        }

        protected virtual double CreateWave()
        {
            return accumulatedCosts.GetAccumulatedCost(CurrentVertex) + 1;
        }

        protected virtual void SpreadWave(IVertex vertex)
        {
            accumulatedCosts.Reevaluate(vertex, CreateWave());
            parentVertices.Add(vertex, CurrentVertex);
        }

        protected bool VertexIsUnwaved(IVertex vertex)
        {
            return accumulatedCosts.GetAccumulatedCost(vertex) == 0;
        }

        protected Queue<IVertex> verticesQueue;

        private void SpreadWaves()
        {
            visitedVertices
                .GetUnvisitedNeighbours(CurrentVertex)
                .Where(VertexIsUnwaved)
                .ForEach(SpreadWave);
        }

        private void ExtractNeighbours()
        {
            var neighbours = visitedVertices.GetUnvisitedNeighbours(CurrentVertex);

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
    }
}
