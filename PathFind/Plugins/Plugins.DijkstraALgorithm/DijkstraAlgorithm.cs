using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using Common.Extensions;
using GraphLib.Base;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Plugins.DijkstraALgorithm
{
    [Description("Dijkstra's algorithm")]
    public class DijkstraAlgorithm : BaseAlgorithm
    {
        public DijkstraAlgorithm() : this(BaseGraph.NullGraph)
        {

        }

        public DijkstraAlgorithm(IGraph graph) : base(graph)
        {
            verticesQueue = new Queue<IVertex>();
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
                    .OrderBy(GetAccumulatedCost)
                    .Where(VertexIsNotVisited)
                    .ToQueue();

                return verticesQueue.DequeueOrDefault();
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

        protected Queue<IVertex> verticesQueue;

        protected void SetVertexAccumulatedCostToInfinity(IVertex vertex)
        {
            accumulatedCosts[vertex.Position] = double.PositiveInfinity;
        }

        protected virtual void RelaxVertex(IVertex vertex)
        {
            var relaxedCost = GetVertexRelaxedCost(vertex);
            if (accumulatedCosts[vertex.Position] > relaxedCost)
            {
                accumulatedCosts[vertex.Position] = relaxedCost;
                parentVertices[vertex.Position] = CurrentVertex;
            }
        }

        private void RelaxNeighbours()
        {
            GetUnvisitedNeighbours(CurrentVertex).ForEach(RelaxVertex);
        }

        private void ExtractNeighbours()
        {
            var neighbours = GetUnvisitedNeighbours(CurrentVertex);

            foreach (var neighbour in neighbours)
            {
                var args = CreateEventArgs(neighbour);
                RaiseOnVertexEnqueuedEvent(args);
                verticesQueue.Enqueue(neighbour);
            }

            verticesQueue = verticesQueue
                .DistinctBy(GetPosition)
                .ToQueue();
        }

        private void SetVerticesAccumulatedCostToInfifnity()
        {
            Graph.Vertices
                .Where(VertexIsNotObstacle)
                .ForEach(SetVertexAccumulatedCostToInfinity);
            accumulatedCosts[endPoints.Start.Position] = 0;
        }
    }
}
