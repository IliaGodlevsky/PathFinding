using Algorithm.Base;
using Algorithm.EventArguments;
using Algorithm.Extensions;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.Algorithms
{
    [Description("Dijkstra's algorithm")]
    public class DijkstraAlgorithm : BaseAlgorithm
    {
        public DijkstraAlgorithm() : this(new NullGraph())
        {

        }

        public DijkstraAlgorithm(IGraph graph) : base(graph)
        {
            verticesQueue = new List<IVertex>();
        }

        public override GraphPath FindPath(IVertex start, IVertex end)
        {
            PrepareForPathfinding(start, end);
            do
            {
                ExtractNeighbours();
                RelaxNeighbours();
                CurrentVertex = NextVertex;
                visitedVertices.Add(CurrentVertex.Position);
                var args = new AlgorithmEventArgs(visitedVertices.Count, false ,CurrentVertex);
                RaiseOnVertexVisitedEvent(args);
            } while (!IsDestination());
            CompletePathfinding();

            return new GraphPath(parentVertices, Start, End);
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour)
        {
            return neighbour.Cost.CurrentCost + accumulatedCosts[CurrentVertex.Position];
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .Where(vertex => !visitedVertices.Contains(vertex.Position))
                    .OrderBy(vertex => accumulatedCosts[vertex.Position])
                    .ToList();

                return verticesQueue.FirstOrDefault();
            }
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            verticesQueue.Clear();
        }

        protected override void PrepareForPathfinding(IVertex start, IVertex end)
        {
            base.PrepareForPathfinding(start, end);
            SetVerticesAccumulatedCostToInfifnity();
        }

        protected List<IVertex> verticesQueue;

        private void RelaxNeighbours()
        {
            GetUnvisitedNeighbours(CurrentVertex).ForEach(neighbour =>
            {
                var relaxedCost = GetVertexRelaxedCost(neighbour);
                if (accumulatedCosts[neighbour.Position] > relaxedCost)
                {
                    accumulatedCosts[neighbour.Position] = relaxedCost;
                    parentVertices[neighbour.Position] = CurrentVertex;
                }
            });
        }

        private void ExtractNeighbours()
        {
            var neighbours = GetUnvisitedNeighbours(CurrentVertex);

            foreach (var neighbour in neighbours)
            {
                bool isExtremeVertex = neighbour.IsEqual(Start) || neighbour.IsEqual(End);
                var args = new AlgorithmEventArgs(visitedVertices.Count, isExtremeVertex, neighbour);
                RaiseOnVertexEnqueuedEvent(args);
                verticesQueue.Add(neighbour);
            }

            verticesQueue = verticesQueue
                .DistinctBy(vert => vert.Position)
                .ToList();
        }

        private void SetVerticesAccumulatedCostToInfifnity()
        {
            Graph
                .Where(vertex => !vertex.IsObstacle)
                .ForEach(vertex => accumulatedCosts[vertex.Position] = double.PositiveInfinity);
            accumulatedCosts[Start.Position] = 0;
        }
    }
}
