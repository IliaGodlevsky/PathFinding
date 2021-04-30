using Algorithm.Base;
using Algorithm.Common;
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Сompanions;
using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plugins.BidirectLeeAlgorithm
{
    [Filterable]
    [ClassName("Lee algorithm (bidirect)")]
    public sealed class BidirectLeeAlgorithm : BaseAlgorithm
    {
        public BidirectLeeAlgorithm(IGraph graph, IEndPoints endPoints)
            : base(graph, endPoints)
        {
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

            return new CombinedGraphPath(firstPath, 
                secondPath, endPoints);
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
            RelaxNeighbours(neighbours, CurrentVertex,
                    accumulatedCosts, parentVertices);

            RelaxNeighbours(secondNeighbours, SecondCurrentVertex,
                secondAccumulatedCosts, secondParentVertices);
        }

        private void NextCurrentVertex()
        {
            CurrentVertex = GetNextVertex(ref verticesQueue, 
                visitedVertices);

            SecondCurrentVertex = GetNextVertex(ref secondVerticesQueue, 
                secondVisitedVertices);
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
                firstPath = new GraphPath(parentVertices, firstEndPoints, graph);
                secondPath = new GraphPath(secondParentVertices, secondEndPoints, graph);
            }

            return base.IsDestination() 
                   || SecondCurrentVertex.IsEqual(endPoints.Start)
                   || SecondCurrentVertex.IsNullObject()
                   || !isNull;
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
            accumulatedCosts = new AccumulatedCosts(graph, 0);
            secondAccumulatedCosts = new AccumulatedCosts(graph, 0);
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

        private IVertex GetNextVertex(ref Queue<IVertex> verticesQueue, VisitedVertices visitedVertices)
        {
            verticesQueue = verticesQueue
                .Where(visitedVertices.IsNotVisited)
                .ToQueue();

            return verticesQueue.DequeueOrDefault();
        }

        private double CreateWave(IAccumulatedCosts accumulatedCosts, IVertex currentVertex)
        {
            return accumulatedCosts.GetAccumulatedCost(currentVertex) + 1;
        }

        private void RelaxNeighbour(IVertex vertex, IVertex currentVertex, 
            IAccumulatedCosts cost, ParentVertices parentVertices)
        {
            cost.Reevaluate(vertex, CreateWave(cost, currentVertex));
            parentVertices.Add(vertex, currentVertex);
        }

        private bool VertexIsUnwaved(IAccumulatedCosts costs, IVertex vertex)
        {
            return costs.GetAccumulatedCost(vertex) == 0;
        }

        private void RelaxNeighbours(IVertex[] neighbours, IVertex currentVertex, 
            IAccumulatedCosts costs, ParentVertices parentVertices)
        {
            foreach(var neighbour in neighbours)
            {
                if (VertexIsUnwaved(costs, neighbour))
                {
                    RelaxNeighbour(neighbour, currentVertex, costs, parentVertices);
                }
            }
        }

        private Queue<IVertex> verticesQueue;
        private Queue<IVertex> secondVerticesQueue;

        private readonly VisitedVertices secondVisitedVertices;
        private IAccumulatedCosts secondAccumulatedCosts;
        private readonly ParentVertices secondParentVertices;

        private IVertex[] neighbours;
        private IVertex[] secondNeighbours;

        private IGraphPath firstPath = new NullGraphPath();
        private IGraphPath secondPath = new NullGraphPath();
        private IVertex intersect = new NullVertex();

        private bool isCheckingIntersectionStopped;
    }
}