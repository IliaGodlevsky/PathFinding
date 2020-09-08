using GraphLibrary.Common.Extensions;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;
using GraphLibrary.Common.Extensions.CollectionExtensions;
using System;
using GraphLibrary.AlgorithmArgs;

namespace GraphLibrary.Algorithm
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

        public Graph Graph { get; set; }

        public LiAlgorithm()
        {
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

        private bool IsDestination(IVertex vertex)
        {
            if (vertex == null || Graph.End == null)
                return true;
            return vertex.IsEnd || !neighbourQueue.Any();
        }

        private readonly Queue<IVertex> neighbourQueue;
    }
}
