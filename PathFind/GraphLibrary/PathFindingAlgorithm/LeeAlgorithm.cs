using GraphLibrary.Graphs;
using System.Collections.Generic;
using System.Linq;
using System;
using GraphLibrary.EventArguments;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;

namespace GraphLibrary.PathFindingAlgorithm
{
    /// <summary>
    /// A wave algorithm (Li algorithm, or wide path find algorithm). 
    /// Uses queue to move next graph top. Finds the shortest path to
    /// the destination top
    /// </summary>
    public class LeeAlgorithm : IPathFindingAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;

        public IGraph Graph { get; set; }

        public LeeAlgorithm()
        {
            Graph = NullGraph.Instance;
            neighbourQueue = new Queue<IVertex>();
        }

        public IEnumerable<IVertex> FindPath()
        {
            OnStarted?.Invoke(this, new AlgorithmEventArgs(Graph));
            var currentVertex = Graph.Start;
            do
            {
                ProcessVertex(currentVertex);
                currentVertex = GetNextVertex();
            } while (!currentVertex.IsEnd);
            OnFinished?.Invoke(this, new AlgorithmEventArgs(Graph));
            return this.GetFoundPath();
        }

        private void ExtractNeighbours(IVertex vertex)
        {
            neighbourQueue.EnqueueRange(vertex.GetUnvisitedNeighbours());

            neighbourQueue = new Queue<IVertex>(neighbourQueue.
                DistinctBy(vert => vert.Position).ToList());
        }

        private IVertex GetNextVertex()
        {           
            neighbourQueue = new Queue<IVertex>(neighbourQueue.
                Where(vertex => !vertex.IsVisited).ToList());

            return neighbourQueue.Dequeue();
        }

        private void SpreadWaves(IVertex vertex)
        {
            vertex.GetUnvisitedNeighbours().ToList().Apply(vert =>
            {
                if (vert.AccumulatedCost == 0)
                {
                    vert.AccumulatedCost = vertex.AccumulatedCost + 1;
                    vert.ParentVertex = vertex;
                }
                return vert;
            });
        }

        private void ProcessVertex(IVertex vertex)
        {
            vertex.IsVisited = true;
            OnVertexVisited?.Invoke(vertex);
            SpreadWaves(vertex);
            ExtractNeighbours(vertex);
        }

        private Queue<IVertex> neighbourQueue;
    }
}
