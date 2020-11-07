using System;
using System.Collections.Generic;
using GraphLib.Vertex.Interface;
using System.Linq;
using System.ComponentModel;
using Algorithm.Extensions;
using Algorithm.EventArguments;
using Algorithm.PathFindingAlgorithms.Interface;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;

namespace Algorithm.PathFindingAlgorithms
{
    /// <summary>
    /// Finds the chippest path to destination vertex. 
    /// </summary>
    [Description("Dijkstra algorithm")]
    public class DijkstraAlgorithm : IPathFindingAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
        public event Action<IVertex> OnEnqueued;

        public IGraph Graph { get; protected set; }

        public bool IsDefault => false;

        public DijkstraAlgorithm(IGraph graph)
        {
            Graph = graph;
            verticesProcessQueue = new List<IVertex>();
        }

        public void FindPath()
        {
            OnStarted?.Invoke(this, new AlgorithmEventArgs(Graph));

            SetAccumulatedCostToInfinity();

            var currentVertex = Graph.Start;
            currentVertex.IsVisited = true;

            do
            {
                ExtractNeighbours(currentVertex);
                RelaxNeighbours(currentVertex);

                currentVertex = GetChippestUnvisitedVertex();
                currentVertex.IsVisited = true;

                OnVertexVisited?.Invoke(currentVertex);
            } while (!currentVertex.IsEnd);

            OnFinished?.Invoke(this, new AlgorithmEventArgs(Graph));
        }

        private void SetAccumulatedCostToInfinity()
        {
            Graph.AsParallel().ForAll(vertex =>
            {
                if (!vertex.IsStart && !vertex.IsObstacle)
                {
                    Graph[vertex.Position].AccumulatedCost = double.PositiveInfinity;
                }
            });
        }

        protected virtual double Relax(IVertex neighbour, IVertex vertex)
        {
            return (int)neighbour.Cost + vertex.AccumulatedCost;
        }

        protected virtual void RelaxNeighbours(IVertex vertex)
        {
            vertex.GetUnvisitedNeighbours().AsParallel().ForAll(neighbour =>
            {
                if (neighbour.AccumulatedCost > Relax(neighbour, vertex))
                {
                    neighbour.AccumulatedCost = Relax(neighbour, vertex);
                    neighbour.ParentVertex = vertex;
                }
            });
        }

        private void ExtractNeighbours(IVertex vertex)
        {
            foreach (var neighbour in vertex.GetUnvisitedNeighbours())
            {
                OnEnqueued?.Invoke(neighbour);
                verticesProcessQueue.Add(neighbour);
            }

            verticesProcessQueue = verticesProcessQueue
                .AsParallel()
                .DistinctBy(vert => vert.Position)
                .ToList();
        }

        protected virtual IVertex GetChippestUnvisitedVertex()
        {
            verticesProcessQueue = verticesProcessQueue
                .AsParallel()
                .Where(vertex => !vertex.IsVisited)
                .OrderBy(vertex => vertex.AccumulatedCost)
                .ToList();

            return verticesProcessQueue.FirstOrDefault();
        }

        protected List<IVertex> verticesProcessQueue;
    }
}
