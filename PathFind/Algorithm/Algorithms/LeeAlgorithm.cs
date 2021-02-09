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

        public override GraphPath FindPath(IVertex start, IVertex end)
        {
            PrepareForPathfinding(start, end);
            do
            {
                ExtractNeighbours();
                SpreadWaves();
                CurrentVertex = NextVertex;
                visitedVerticesCoordinates.Add(CurrentVertex.Position);
                var args = CreateEventArgs(CurrentVertex);
                RaiseOnVertexVisitedEvent(args);
            } while (!IsDestination());
            CompletePathfinding();

            return new GraphPath(parentVertices, Start, End, visitedVerticesCoordinates.Count);
        }

        protected override void PrepareForPathfinding(IVertex start, IVertex end)
        {
            base.PrepareForPathfinding(start, end);
            SetVerticesAccumulatedCostToZero();
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
                    .Where(vertex => !visitedVerticesCoordinates.Contains(vertex.Position))
                    .ToQueue();

                return verticesQueue.DequeueOrDefault();
            }
        }

        protected virtual double CreateWave()
        {
            return accumulatedCosts[CurrentVertex.Position] + 1;
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

            verticesQueue = verticesQueue
                .DistinctBy(vert => vert.Position)
                .ToQueue();
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
