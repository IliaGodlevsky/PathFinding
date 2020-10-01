using GraphLibrary.Graphs;
using System;
using System.Collections.Generic;
using GraphLibrary.EventArguments;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;
using System.Linq;

namespace GraphLibrary.PathFindingAlgorithm
{
    /// <summary>
    /// Finds the chippest path to destination vertex. 
    /// </summary>
    public class DijkstraAlgorithm : IPathFindingAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
        public event Action<IVertex> OnEnqueued;

        public IGraph Graph { get; set; }

        public DijkstraAlgorithm()
        {
            Graph = NullGraph.Instance;
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
            Graph.Array.Apply(vertex =>
            {
                if (!vertex.IsStart && !vertex.IsObstacle)
                    vertex.AccumulatedCost = double.PositiveInfinity;
                return vertex;
            });
        }

        protected virtual double Relax(IVertex neighbour, IVertex vertex)
        {
            return neighbour.Cost + vertex.AccumulatedCost;
        }

        protected virtual void RelaxNeighbours(IVertex vertex)
        {
            vertex.Neighbours.Apply(neighbour =>
            {
                if (neighbour.AccumulatedCost > Relax(neighbour, vertex))
                {
                    neighbour.AccumulatedCost = Relax(neighbour, vertex);
                    neighbour.ParentVertex = vertex;
                }
                return neighbour;
            });
        }

        private void ExtractNeighbours(IVertex vertex)
        {
            vertex.GetUnvisitedNeighbours().ToList().Apply(neighbour =>
            {
                OnEnqueued?.Invoke(neighbour);
                verticesProcessQueue.Add(neighbour);
                return neighbour;
            });
            verticesProcessQueue = verticesProcessQueue.DistinctBy(vert => vert.Position).ToList();
        }

        protected virtual IVertex GetChippestUnvisitedVertex()
        {
            verticesProcessQueue.RemoveAll(vertex => vertex.IsVisited);
            verticesProcessQueue.Sort((v1, v2) => v1.AccumulatedCost.CompareTo(v2.AccumulatedCost));
            return verticesProcessQueue.FirstOrNullVertex();
        }

        protected List<IVertex> verticesProcessQueue;
    }
}
