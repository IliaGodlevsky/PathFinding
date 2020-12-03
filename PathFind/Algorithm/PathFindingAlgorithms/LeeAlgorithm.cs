using System.Collections.Generic;
using System.Linq;
using GraphLib.Vertex.Interface;
using System.ComponentModel;
using Algorithm.Extensions;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using Algorithm.PathFindingAlgorithms.Abstractions;

namespace Algorithm.PathFindingAlgorithms
{
    /// <summary>
    /// A wave algorithm (Lee algorithm, or width-first pathfinding algorithm). 
    /// Uses queue to move next vertex. Finds the shortest path (in steps) to
    /// the destination top
    /// </summary>
    [Description("Lee algorithm")]
    public class LeeAlgorithm : BaseAlgorithm
    {
        public LeeAlgorithm(IGraph graph) : base(graph)
        {
            verticesQueue = new Queue<IVertex>();
        }

        public override void FindPath()
        {
            BeginPathfinding();
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

        protected virtual double WaveFunction(IVertex vertex)
        {
            return vertex.AccumulatedCost + 1;
        }

        private void SpreadWaves()
        {
            CurrentVertex.GetUnvisitedNeighbours().AsParallel().ForAll(neighbour =>
            {
                if (neighbour.AccumulatedCost == 0)
                {
                    neighbour.AccumulatedCost = WaveFunction(CurrentVertex);
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
