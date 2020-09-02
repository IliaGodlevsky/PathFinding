using GraphLibrary.Common.Extensions;
using GraphLibrary.Extensions;
using GraphLibrary.Graph;
using GraphLibrary.PauseMaker;
using GraphLibrary.Statistics;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Algorithm
{
    /// <summary>
    /// A wave algorithm (Li algorithm, or wide path find algorithm). 
    /// Uses queue to move next graph top. Finds the shortest path to
    /// the destination top
    /// </summary>
    public class WidePathFindAlgorithm : IPathFindAlgorithm
    {       
        public IStatisticsCollector StatCollector { get; set; }
        public AbstractGraph Graph { get; set; }
        public IPauseProvider Pauser { get; set; }

        public WidePathFindAlgorithm()
        {
            neighbourQueue = new Queue<IVertex>();
            StatCollector = new StatisticsCollector();
        }

        public void DrawPath() => this.DrawPath(FollowWave);

        public void FindDestionation()
        {
            if (this.IsRightGraphSettings())
            {
                StatCollector.StartCollect();
                var currentVertex = Graph.Start;
                ProcessVertex(currentVertex);
                while (!IsDestination(currentVertex))
                {
                    currentVertex = neighbourQueue.Dequeue();
                    if (!currentVertex.IsVisited)
                        ProcessVertex(currentVertex);
                }
                StatCollector.StopCollect();
            }
        }

        private IVertex FollowWave(IVertex vertex)
        {
            bool IsWaveVertex(IVertex vert) => vert.IsVisited && !vert.IsEnd;
            double min = vertex.Neighbours.Min(vert => vert.AccumulatedCost);
            return vertex.Neighbours.Find(vert => min == vert.AccumulatedCost
                   && IsWaveVertex(vert));
        }

        private void ExtractNeighbours(IVertex vertex)
        {
            neighbourQueue.EnqueueRange(vertex.GetUnvisitedNeighbours());
        }

        private void SpreadWaves(IVertex vertex)
        {
            foreach (var neighbour in vertex.Neighbours)
            {
                if (!neighbour.IsVisited)
                    neighbour.AccumulatedCost = vertex.AccumulatedCost + 1;
            }
        }

        private void ProcessVertex(IVertex vertex)
        {
            this.VisitVertex(vertex);
            SpreadWaves(vertex);
            ExtractNeighbours(vertex);
        }

        private bool IsDestination(IVertex vertex)
        {
            if (vertex == null || Graph.End == null) 
                return true;
            return vertex.IsEnd || !neighbourQueue.Any();
        }

        protected Queue<IVertex> neighbourQueue;
    }
}
