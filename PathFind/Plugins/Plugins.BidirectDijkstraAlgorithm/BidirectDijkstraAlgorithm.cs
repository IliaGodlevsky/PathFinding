using Algorithm.Base;
using Algorithm.Common;
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions;
using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plugins.BidirectDijkstraAlgorithm
{
    [ClassName("Dijkstra's algorithm (bidirect)")]
    public class BidirectDijkstraAlgorithm : BaseAlgorithm
    {
        public BidirectDijkstraAlgorithm(IGraph graph, IEndPoints endPoints) 
            : this(graph, endPoints, new DefaultStepRule())
        {

        }

        public BidirectDijkstraAlgorithm(IGraph graph, IEndPoints endPoints, IStepRule stepRule)
            : base(graph, endPoints)
        {
            this.stepRule = stepRule;
            verticesQueue = new Queue<IVertex>();
            secondVerticesQueue = new Queue<IVertex>();
            secondParentVertices = new ParentVertices();
            secondVisitedVertices = new VisitedVertices();
        }

        public override IGraphPath FindPath()
        {
            PrepareForPathfinding();
            StartCheckingIntersection();
            do
            {
                GetUnvisitedNeighbours();
                ExtractCurrentVertexNeighbours();
                RelaxCurrentVertexNeighbours();
                NextCurrentVertex();
                VisitCurrentVertex();

            } while (!IsDestination());
            CompletePathfinding();

            return graphPath;
        }

        private void GetUnvisitedNeighbours()
        {
            neighbours = GetCurrentVertexNeighbours(
                visitedVertices, CurrentVertex);
            secondNeighbours = GetCurrentVertexNeighbours(
                secondVisitedVertices, SecondCurrentVertex);
        }

        private void ExtractCurrentVertexNeighbours()
        {
            ExtractNeighbours(ref verticesQueue, neighbours);
            ExtractNeighbours(ref secondVerticesQueue, secondNeighbours);
        }

        private void RelaxCurrentVertexNeighbours()
        {
            RelaxNeighbours(neighbours, CurrentVertex, accumulatedCosts, parentVertices);
            RelaxNeighbours(secondNeighbours, SecondCurrentVertex, secondAccumulatedCosts, secondParentVertices);
        }

        private void NextCurrentVertex()
        {
            CurrentVertex = GetNextVertex(ref verticesQueue, visitedVertices, accumulatedCosts);
            SecondCurrentVertex = GetNextVertex(ref secondVerticesQueue, secondVisitedVertices, secondAccumulatedCosts);
        }

        private void VisitCurrentVertex()
        {
            VisitVertex(visitedVertices, CurrentVertex);
            VisitVertex(secondVisitedVertices, SecondCurrentVertex);
        }

        protected override AlgorithmEventArgs CreateEventArgs(IVertex vertex)
        {
            return new AlgorithmEventArgs(visitedVertices.Count
                + secondVisitedVertices.Count, endPoints, vertex);
        }

        protected override void Reset()
        {
            base.Reset();
            secondVisitedVertices.Clear();
            secondParentVertices.Clear();
            isCheckingIntersectionStopped = false;
        }

        private void StartCheckingIntersection()
        {
            Task.Run(() =>
            {
                while (!isCheckingIntersectionStopped)
                {
                    intersect = secondVisitedVertices.Intersect(visitedVertices);
                }
            });
        }

        protected override bool IsDestination()
        {
            bool isNull = intersect.IsNullObject();
            if (!isNull)
            {
                var firstEndPoints = new EndPoints(endPoints.Start, intersect);
                var secondEndPoints = new EndPoints(endPoints.End, intersect);
                var firstPath = new GraphPath(parentVertices, firstEndPoints, graph);
                var secondPath = new GraphPath(secondParentVertices, secondEndPoints, graph);
                graphPath = new CombinedGraphPath(firstPath, secondPath, secondEndPoints, stepRule);
                return true;
            }

            return base.IsDestination()
                || SecondCurrentVertex.IsEqual(endPoints.Start)
                || SecondCurrentVertex.IsNullObject();
        }

        private void VisitVertex(VisitedVertices visitedVertices, IVertex vertex)
        {
            visitedVertices.Add(vertex);
            var args = CreateEventArgs(vertex);
            RaiseOnVertexVisitedEvent(args);
        }

        private IVertex SecondCurrentVertex { get; set; }

        private void ExtractNeighbours(ref Queue<IVertex> verticesQueue,
            IVertex[] neighbours)
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

        private IVertex[] GetCurrentVertexNeighbours(VisitedVertices visitedVertices,
            IVertex currentVertex)
        {
            return visitedVertices
                .GetUnvisitedNeighbours(currentVertex)
                .ToArray();
        }

        protected override void PrepareForPathfinding()
        {
            base.PrepareForPathfinding();
            SecondCurrentVertex = endPoints.End;
            secondVisitedVertices.Add(SecondCurrentVertex);
            var args = CreateEventArgs(SecondCurrentVertex);
            RaiseOnAlgorithmStartedEvent(args);
            accumulatedCosts =
                new AccumulatedCostsWithExcept(
                    new AccumulatedCosts(graph, double.PositiveInfinity), endPoints.Start);
            secondAccumulatedCosts = 
                new AccumulatedCostsWithExcept(
                    new AccumulatedCosts(graph, double.PositiveInfinity), endPoints.End);
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            var args = CreateEventArgs(SecondCurrentVertex);
            RaiseOnAlgorithmFinishedEvent(args);
            verticesQueue.Clear();
            secondVerticesQueue.Clear();
            isCheckingIntersectionStopped = true;
        }

        private IVertex GetNextVertex(ref Queue<IVertex> verticesQueue, 
            VisitedVertices visitedVertices, IAccumulatedCosts costs)
        {
            verticesQueue = verticesQueue
                    .OrderBy(costs.GetAccumulatedCost)
                    .Where(visitedVertices.IsNotVisited)
                    .ToQueue();

            return verticesQueue.DequeueOrDefault();
        }

        protected virtual void RelaxVertex(IVertex vertex, IAccumulatedCosts costs, 
            IVertex currentVertex, ParentVertices parentVertices)
        {
            var relaxedCost = GetVertexRelaxedCost(vertex, costs, currentVertex);
            if (costs.Compare(vertex, relaxedCost) > 0)
            {
                costs.Reevaluate(vertex, relaxedCost);
                parentVertices.Add(vertex, currentVertex);
            }
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour, 
            IAccumulatedCosts costs, IVertex currentVertex)
        {
            return stepRule.CalculateStepCost(neighbour, currentVertex)
                   + costs.GetAccumulatedCost(currentVertex);
        }

        private void RelaxNeighbours(IVertex[] neighbours, IVertex currentVertex,
            IAccumulatedCosts costs, ParentVertices parentVertices)
        {
            foreach (var neighbour in neighbours)
            {
                RelaxVertex(neighbour, costs, currentVertex, parentVertices);
            }
        }

        private Queue<IVertex> verticesQueue;
        private Queue<IVertex> secondVerticesQueue;
        private readonly VisitedVertices secondVisitedVertices;
        private IAccumulatedCosts secondAccumulatedCosts;
        private readonly ParentVertices secondParentVertices;
        private IVertex[] neighbours;
        private IVertex[] secondNeighbours;
        private IVertex intersect = new NullVertex();
        private IGraphPath graphPath = new NullGraphPath();
        private bool isCheckingIntersectionStopped;
        private readonly IStepRule stepRule;
    }
}
