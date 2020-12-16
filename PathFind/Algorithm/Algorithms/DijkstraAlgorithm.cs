using Algorithm.Algorithms.Abstractions;
using Algorithm.Extensions;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.Algorithms
{
    [Description("Dijkstra's algorithm")]
    public class DijkstraAlgorithm : BaseAlgorithm
    {
        public DijkstraAlgorithm(IGraph graph) : base(graph)
        {
            verticesQueue = new List<IVertex>();
        }

        public override void FindPath()
        {
            PrepareForPathfinding();
            do
            {
                ExtractNeighbours();
                RelaxNeighbours();
                CurrentVertex = NextVertex;
                CurrentVertex.IsVisited = true;
                RaiseOnVertexVisitedEvent();
            } while (!IsDestination);
            CompletePathfinding();
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour)
        {
            return (int)neighbour.Cost + CurrentVertex.AccumulatedCost;
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .AsParallel()
                    .Where(vertex => !vertex.IsVisited)
                    .OrderBy(vertex => vertex.AccumulatedCost)
                    .ToList();

                return verticesQueue.FirstOrDefault();
            }
        }

        protected override void PrepareForPathfinding()
        {
            base.PrepareForPathfinding();
            SetVerticesAccumulatedCost();
        }

        protected override void CompletePathfinding()
        {
            verticesQueue.Clear();
            base.CompletePathfinding();
        }

        protected List<IVertex> verticesQueue;

        private void RelaxNeighbours()
        {
            CurrentVertex.GetUnvisitedNeighbours().AsParallel().ForAll(neighbour =>
            {
                var relaxedCost = GetVertexRelaxedCost(neighbour);
                if (neighbour.AccumulatedCost > relaxedCost)
                {
                    neighbour.AccumulatedCost = relaxedCost;
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
                verticesQueue.Add(neighbour);
            }

            verticesQueue = verticesQueue
                .AsParallel()
                .DistinctBy(vert => vert.Position)
                .ToList();
        }

        private void SetVerticesAccumulatedCost(double accumulatedCost
            = double.PositiveInfinity)
        {
            Graph.AsParallel().ForAll(vertex =>
            {
                if (!vertex.IsStart && !vertex.IsObstacle)
                {
                    var coordinate = vertex.Position;
                    Graph[coordinate].AccumulatedCost = accumulatedCost;
                }
            });
        }
    }
}
