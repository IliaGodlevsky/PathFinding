using GraphLibrary.Extensions;
using GraphLibrary.Graphs;
using System.Collections.Generic;
using System.Linq;
using System;
using GraphLibrary.EventArguments;
using GraphLibrary.Extensions.CollectionExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.PathFindingAlgorithm
{
    /// <summary>
    /// A wave algorithm (Li algorithm, or wide path find algorithm). 
    /// Uses queue to move next graph top. Finds the shortest path to
    /// the destination top
    /// </summary>
    public class LiAlgorithm : IPathFindingAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;

        public IGraph Graph { get; set; }

        public LiAlgorithm()
        {
            Graph = NullGraph.GetInstance();
            neighbourQueue = new Queue<IVertex>();
        }

        public IEnumerable<IVertex> FindPath()
        {
            OnStarted?.Invoke(this,
                new AlgorithmEventArgs(Graph));
            var currentVertex = Graph.Start;
            ProcessVertex(currentVertex);
            while (!currentVertex.IsEnd)
            {
                currentVertex = neighbourQueue.DequeueSecure();
                if (!currentVertex.IsVisited)
                    ProcessVertex(currentVertex);
            }
            OnFinished?.Invoke(this,
                new AlgorithmEventArgs(Graph));
            return this.GetFoundPath();
        }

        private void ExtractNeighbours(IVertex vertex)
        {
            neighbourQueue.EnqueueRange(vertex.GetUnvisitedNeighbours());
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
            OnVertexVisited?.Invoke(vertex);
            SpreadWaves(vertex);
            ExtractNeighbours(vertex);
        }

        private readonly Queue<IVertex> neighbourQueue;
    }
}
