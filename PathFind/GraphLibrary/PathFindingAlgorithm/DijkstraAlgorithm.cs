using GraphLibrary.Graphs;
using System;
using System.Collections.Generic;
using GraphLibrary.EventArguments;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;

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

        public Func<IVertex, IVertex, double> RelaxFunction { private get; set; }
        public IGraph Graph { get; set; }

        public DijkstraAlgorithm()
        {
            Graph = NullGraph.Instance;
            verticesProessQueue = new List<IVertex>();
            RelaxFunction = (neighbour, vertex)
                => neighbour.Cost + vertex.AccumulatedCost;
        }

        public IEnumerable<IVertex> FindPath()
        {
            OnStarted?.Invoke(this,
                new AlgorithmEventArgs(Graph));
            SetAccumulatedCostToInfinity();
            var currentVertex = Graph.Start;
            currentVertex.IsVisited = true;
            do
            {
                ExtractNeighbours(currentVertex);
                SpreadRelaxWave(currentVertex);
                currentVertex = GetChippestUnvisitedVertex();
                currentVertex.IsVisited = true;
                OnVertexVisited?.Invoke(currentVertex);
            } while (!currentVertex.IsEnd);
            OnFinished?.Invoke(this,
                new AlgorithmEventArgs(Graph));
            return this.GetFoundPath();
        }

        private void SetAccumulatedCostToInfinity()
        {
            IVertex SetValueToInfinity(IVertex vertex)
            {
                if (!vertex.IsStart && !vertex.IsObstacle)
                    vertex.AccumulatedCost = double.PositiveInfinity;
                return vertex;
            }
            Graph.Array.Apply(SetValueToInfinity);
        }

        private void SpreadRelaxWave(IVertex vertex)
        {
            foreach (var neighbour in vertex.Neighbours)
            {
                var relaxFunctionResult = RelaxFunction(neighbour, vertex);
                if (neighbour.AccumulatedCost > relaxFunctionResult)
                {
                    neighbour.AccumulatedCost = relaxFunctionResult;
                    neighbour.ParentVertex = vertex;
                }
            }
        }

        private void ExtractNeighbours(IVertex vertex)
        {
            verticesProessQueue.AddRange(vertex.GetUnvisitedNeighbours());
        }

        private IVertex GetChippestUnvisitedVertex()
        {
            verticesProessQueue.RemoveAll(vertex => vertex.IsVisited);
            verticesProessQueue.Sort((vertex1, vertex2)
                => vertex1.AccumulatedCost.CompareTo(vertex2.AccumulatedCost));
            return verticesProessQueue.FirstOrNullVertex();
        }

        private readonly List<IVertex> verticesProessQueue;
    }
}
