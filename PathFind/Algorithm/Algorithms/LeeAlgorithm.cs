using System.Collections.Generic;
using System.Linq;
using GraphLib.Vertex.Interface;
using System.ComponentModel;
using Algorithm.Extensions;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using Algorithm.Algorithms.Abstractions;

namespace Algorithm.Algorithms
{
    [Description("Lee algorithm")]
    public class LeeAlgorithm : BaseAlgorithm
    {
        public LeeAlgorithm(IGraph graph) : base(graph)
        {
            verticesQueue = new Queue<IVertex>();
        }

        public override void FindPath()
        {
            PrepareForPathfinding();
            do
            {
                ExtractNeighbours();
                SpreadWaves();
                CurrentVertex = NextVertex;
                CurrentVertex.IsVisited = true;
                RaiseOnVertexVisitedEvent();
            } while (!IsDestination);
            CompletePathfinding();
        }

        protected override IVertex NextVertex
        {
            get
            {
                var notVisitedVertices = verticesQueue.
                    Where(vertex => !vertex.IsVisited);
                verticesQueue = new Queue<IVertex>(notVisitedVertices);

                return verticesQueue.DequeueOrDefault();
            }
        }

        protected override void CompletePathfinding()
        {
            verticesQueue.Clear();
            base.CompletePathfinding();
        }

        protected virtual double CreateWave()
        {
            return CurrentVertex.AccumulatedCost + 1;
        }

        private void SpreadWaves()
        {
            CurrentVertex.GetUnvisitedNeighbours().AsParallel().ForAll(neighbour =>
            {
                if (neighbour.AccumulatedCost == 0)
                {
                    neighbour.AccumulatedCost = CreateWave();
                    neighbour.ParentVertex = CurrentVertex;
                }
            });
        }

        private void ExtractNeighbours()
        {
            var neighbours = CurrentVertex.GetUnvisitedNeighbours();

            foreach (var neighbour in neighbours)
            {
                RaiseOnVertexEnqueuedEvent(neighbour);
                verticesQueue.Enqueue(neighbour);
            }

            var distinctedVertices = verticesQueue.
                DistinctBy(vert => vert.Position);
            verticesQueue = new Queue<IVertex>(distinctedVertices);
        }

        protected Queue<IVertex> verticesQueue;
    }
}
