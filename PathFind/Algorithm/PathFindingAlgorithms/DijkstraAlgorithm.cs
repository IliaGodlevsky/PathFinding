using System.Collections.Generic;
using GraphLib.Vertex.Interface;
using System.Linq;
using System.ComponentModel;
using Algorithm.Extensions;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using Algorithm.PathFindingAlgorithms.Abstractions;

namespace Algorithm.PathFindingAlgorithms
{
    /// <summary>
    /// Finds the chippest path to destination vertex. 
    /// </summary>
    [Description("Dijkstra algorithm")]
    internal class DijkstraAlgorithm : BaseAlgorithm
    {
        public DijkstraAlgorithm(IGraph graph) : base(graph)
        {
            verticesQueue = new List<IVertex>();
        }

        public override void FindPath()
        {
            BeginPathfinding();
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

        protected virtual double GetVertexRelaxedCost(
            IVertex neighbour, IVertex vertex)
        {
            return (int)neighbour.Cost + vertex.AccumulatedCost;
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

        protected override void BeginPathfinding()
        {
            base.BeginPathfinding();
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
                var relaxedCost = GetVertexRelaxedCost(neighbour, CurrentVertex);
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
