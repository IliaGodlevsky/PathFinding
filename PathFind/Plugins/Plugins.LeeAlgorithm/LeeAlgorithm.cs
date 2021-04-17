using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using Common.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Plugins.LeeAlgorithm
{
    [Description("Lee algorithm")]
    public class LeeAlgorithm : BaseAlgorithm
    {
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
                verticesQueue = verticesQueue
                    .Where(VertexIsNotVisited)
                    .ToQueue();
                return verticesQueue.DequeueOrDefault();
            }
        }

        protected virtual double CreateWave()
        {
            return GetAccumulatedCost(CurrentVertex) + 1;
        }

        protected virtual void SpreadWave(IVertex vertex)
        {
            accumulatedCosts[vertex.Position] = CreateWave();
            parentVertices[vertex.Position] = CurrentVertex;
        }

        protected bool VertexIsUnwaved(IVertex vertex)
        {
            return accumulatedCosts[vertex.Position] == 0;
        }

        protected void SetVertexAccumulatedCostToZero(IVertex vertex)
        {
            accumulatedCosts[vertex.Position] = 0;
        }

        protected Queue<IVertex> verticesQueue;

        private void SpreadWaves()
        {
            GetUnvisitedNeighbours(CurrentVertex)
                .Where(VertexIsUnwaved)
                .ForEach(SpreadWave);
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

        private void SetVerticesAccumulatedCostToZero()
        {
            graph.Vertices
                .Where(VertexIsNotObstacle)
                .ForEach(SetVertexAccumulatedCostToZero);
        }
    }
}
