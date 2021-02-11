using Algorithm.Base;
using Algorithm.Extensions;
using Common.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using GraphLib.NullObjects;
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

        public override IGraphPath FindPath(IEndPoints endpoints)
        {
            PrepareForPathfinding(endpoints);
            do
            {
                ExtractNeighbours();
                RelaxNeighbours();
                CurrentVertex = NextVertex;
                VisitVertex(CurrentVertex);
            } while (!IsDestination());
            CompletePathfinding();

            return new GraphPath(parentVertices, endpoints);
        }

        protected void VisitVertex(IVertex vertex)
        {
            visitedVertices[vertex.Position] = vertex;
            var args = CreateEventArgs(vertex);
            RaiseOnVertexVisitedEvent(args);
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour)
        {
            return neighbour.Cost.CurrentCost + GetAccumulatedCost(CurrentVertex);
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .Where(IsNotVisited)
                    .OrderBy(GetAccumulatedCost)
                    .ToList();

                return verticesQueue.FirstOrDefault();
            }
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            verticesQueue.Clear();
        }

        protected override void PrepareForPathfinding(IEndPoints endpoints)
        {
            base.PrepareForPathfinding(endpoints);
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
                var args = CreateEventArgs(neighbour);
                RaiseOnVertexEnqueuedEvent(args);
                verticesQueue.Add(neighbour);
            }

            verticesQueue = verticesQueue.DistinctBy(GetPosition).ToList();
        }

        private void SetVerticesAccumulatedCostToInfifnity()
        {
            Graph
                .Where(vertex => !vertex.IsObstacle)
                .ForEach(vertex => accumulatedCosts[vertex.Position] = double.PositiveInfinity);
            accumulatedCosts[endPoints.Start.Position] = 0;
        }
    }
}
