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

namespace Plugins.DijkstraALgorithm
{
    [ClassName("Dijkstra's algorithm")]
    public class DijkstraAlgorithm : BaseAlgorithm
    {
        public DijkstraAlgorithm(IGraph graph, IEndPoints endPoints) 
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
                RelaxNeighbours();
                CurrentVertex = NextVertex;
                VisitVertex(CurrentVertex);
            } while (!IsDestination());

            CompletePathfinding();

            return new GraphPath(parentVertices, endPoints, graph);
        }

        protected void VisitVertex(IVertex vertex)
        {
            visitedVertices.Add(vertex);
            var args = CreateEventArgs(vertex);
            RaiseOnVertexVisitedEvent(args);
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour)
        {
            return neighbour.Cost.CurrentCost + accumulatedCosts.GetAccumulatedCost(CurrentVertex);
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .OrderBy(accumulatedCosts.GetAccumulatedCost)
                    .Where(visitedVertices.IsNotVisited)
                    .ToQueue();

                return verticesQueue.DequeueOrDefault();
            }
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            verticesQueue.Clear();
        }

        protected override void PrepareForPathfinding()
        {
            base.PrepareForPathfinding();
            accumulatedCosts =
                new AccumulatedCostsWithExcept(
                    new AccumulatedCosts(graph, double.PositiveInfinity), endPoints.Start);
        }

        protected Queue<IVertex> verticesQueue;

        protected virtual void RelaxVertex(IVertex vertex)
        {
            var relaxedCost = GetVertexRelaxedCost(vertex);
            if (accumulatedCosts.Compare(vertex, relaxedCost) > 0)
            {
                accumulatedCosts.Reevaluate(vertex, relaxedCost);
                parentVertices.Add(vertex, CurrentVertex);
            }
        }

        private void RelaxNeighbours()
        {
            visitedVertices.GetUnvisitedNeighbours(CurrentVertex).ForEach(RelaxVertex);
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
