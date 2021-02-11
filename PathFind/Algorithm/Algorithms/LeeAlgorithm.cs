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
    [Description("Lee algorithm")]
    public class LeeAlgorithm : BaseAlgorithm
    {
        public LeeAlgorithm() : this(new NullGraph())
        {

        }

        public LeeAlgorithm(IGraph graph) : base(graph)
        {
            verticesQueue = new Queue<IVertex>();
        }

        public override IGraphPath FindPath(IEndPoints endpoints)
        {
            PrepareForPathfinding(endpoints);
            do
            {
                ExtractNeighbours();
                SpreadWaves();
                CurrentVertex = NextVertex;
                VisitVertex(CurrentVertex);
            } while (!IsDestination());
            CompletePathfinding();

            return new GraphPath(parentVertices, endpoints);
        }

        protected override void PrepareForPathfinding(IEndPoints endpoints)
        {
            base.PrepareForPathfinding(endpoints);
            SetVerticesAccumulatedCostToZero();
        }

        protected virtual void VisitVertex(IVertex vertex)
        {
            visitedVertices[vertex.Position] = vertex;
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
                verticesQueue = verticesQueue.Where(IsNotVisited).ToQueue();
                return verticesQueue.DequeueOrDefault();
            }
        }

        protected virtual double CreateWave()
        {
            return GetAccumulatedCost(CurrentVertex) + 1;
        }

        private void SpreadWaves()
        {
            GetUnvisitedNeighbours(CurrentVertex)
                .Where(neighbour => accumulatedCosts[neighbour.Position] == 0)
                .ForEach(neighbour =>
                {
                    accumulatedCosts[neighbour.Position] = CreateWave();
                    parentVertices[neighbour.Position] = CurrentVertex;
                });
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

            verticesQueue = verticesQueue.DistinctBy(GetPosition).ToQueue();
        }

        private void SetVerticesAccumulatedCostToZero()
        {
            Graph
                .Where(vertex => !vertex.IsObstacle)
                .ForEach(vertex => accumulatedCosts[vertex.Position] = 0);
        }

        protected Queue<IVertex> verticesQueue;
    }
}
