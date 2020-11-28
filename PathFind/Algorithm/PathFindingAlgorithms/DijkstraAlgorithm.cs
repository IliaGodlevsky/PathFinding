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
using Algorithm.Delegates;

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
        public event Action<IVertex> OnVertexEnqueued;

        public IGraph Graph { get; protected set; }

        public bool IsDefault => false;

        public DijkstraAlgorithm(IGraph graph)
        {
            Graph = graph;
            verticesQueue = new List<IVertex>();
        }

        public virtual void FindPath()
        {
            var args = new AlgorithmEventArgs(Graph);
            OnStarted?.Invoke(this, args);
            SetVerticesAccumulatedCost();
            var currentVertex = Graph.Start;
            currentVertex.IsVisited = true;
            do
            {
                ExtractNeighbours(currentVertex);
                RelaxNeighbours(currentVertex);
                currentVertex = ChippestUnvisitedVertex;
                currentVertex.IsVisited = true;
                OnVertexVisited?.Invoke(currentVertex);
            } while (!currentVertex.IsEnd);
            verticesQueue.Clear();
            args = new AlgorithmEventArgs(Graph);
            OnFinished?.Invoke(this, args);
        }

        private void SetVerticesAccumulatedCost(double accumulatedCost = double.PositiveInfinity)
        {
            Graph.AsParallel().ForAll(vertex =>
            {
                if (!vertex.IsStart && !vertex.IsObstacle)
                {
                    Graph[vertex.Position].AccumulatedCost = accumulatedCost;
                }
            });
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour, IVertex vertex)
        {
            return (int)neighbour.Cost + vertex.AccumulatedCost;
        }

        protected virtual void RelaxNeighbours(IVertex vertex)
        {
            vertex.GetUnvisitedNeighbours().AsParallel().ForAll(neighbour=> 
            {
                var relaxedCost = GetVertexRelaxedCost(neighbour, vertex);
                if (neighbour.AccumulatedCost > relaxedCost)
                {
                    neighbour.AccumulatedCost = relaxedCost;
                    neighbour.ParentVertex = vertex;
                }
            });
        }

        protected virtual void ExtractNeighbours(IVertex vertex)
        {
            foreach(var neighbour in vertex.GetUnvisitedNeighbours())
            {
                OnVertexEnqueued?.Invoke(neighbour);
                verticesQueue.Add(neighbour);
            }

            verticesQueue = verticesQueue
                .AsParallel()
                .DistinctBy(vert => vert.Position)
                .ToList();
        }

        protected virtual IVertex ChippestUnvisitedVertex
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

        protected List<IVertex> verticesQueue;
    }
}
