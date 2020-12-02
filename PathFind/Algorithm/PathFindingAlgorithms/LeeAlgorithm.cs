using System.Collections.Generic;
using System.Linq;
using System;
using GraphLib.Vertex.Interface;
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
    /// A wave algorithm (Lee algorithm, or width-first pathfinding algorithm). 
    /// Uses queue to move next vertex. Finds the shortest path (in steps) to
    /// the destination top
    /// </summary>
    [Description("Lee algorithm")]
    public class LeeAlgorithm : IAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
        public event Action<IVertex> OnVertexEnqueued;

        public IGraph Graph { get; protected set; }

        public bool IsDefault => false;

        public LeeAlgorithm(IGraph graph)
        {
            Graph = graph;
            verticesQueue = new Queue<IVertex>();
        }

        public void FindPath()
        {
            var args = new AlgorithmEventArgs(Graph);
            OnStarted?.Invoke(this, args);
            var currentVertex = Graph.Start;
            ProcessVertex(currentVertex);
            while (!currentVertex.IsEnd)
            {
                currentVertex = GetNextVertex();
                ProcessVertex(currentVertex);
            }
            verticesQueue.Clear();
            args = new AlgorithmEventArgs(Graph);
            OnFinished?.Invoke(this, args);
        }

        protected virtual IVertex GetNextVertex()
        {
            var notVisitedVertices = verticesQueue.
                Where(vertex => !vertex.IsVisited);
            verticesQueue = new Queue<IVertex>(notVisitedVertices);

            return verticesQueue.DequeueOrDefault();
        }

        protected virtual double WaveFunction(IVertex vertex)
        {
            return vertex.AccumulatedCost + 1;
        }

        private void SpreadWaves(IVertex vertex)
        {
            vertex.GetUnvisitedNeighbours().AsParallel().ForAll(neighbour =>
            {
                if (neighbour.AccumulatedCost == 0)
                {
                    neighbour.AccumulatedCost = WaveFunction(vertex);
                    neighbour.ParentVertex = vertex;
                }
            });
        }

        private void ExtractNeighbours(IVertex vertex)
        {
            foreach (var vert in vertex.GetUnvisitedNeighbours())
            {
                OnVertexEnqueued?.Invoke(vert);
                verticesQueue.Enqueue(vert);
            }

            var distinctedVertices = verticesQueue.
                DistinctBy(vert => vert.Position);
            verticesQueue = new Queue<IVertex>(distinctedVertices);
        }

        private void ProcessVertex(IVertex vertex)
        {
            vertex.IsVisited = true;
            OnVertexVisited?.Invoke(vertex);
            SpreadWaves(vertex);
            ExtractNeighbours(vertex);
        }

        protected Queue<IVertex> verticesQueue;
    }
}
