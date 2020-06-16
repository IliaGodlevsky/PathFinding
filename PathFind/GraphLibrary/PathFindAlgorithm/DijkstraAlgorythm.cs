using GraphLibrary.Graph;
using GraphLibrary.Statistics;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Algorithm
{
    /// <summary>
    /// Finds the chippest path to destination top. 
    /// </summary>
    public class DijkstraAlgorithm : IPathFindAlgorithm
    {
        protected readonly AbstractGraph graph;
        private readonly List<IVertex> neigbourQueue = new List<IVertex>();
        public AbstractStatisticsCollector StatCollector { get; set; }

        public DijkstraAlgorithm(AbstractGraph graph)
        {
            this.graph = graph;
            foreach (var vertex in graph)
                (vertex as IVertex).Value = double.PositiveInfinity;
            StatCollector = new WeightedGraphSearchAlgoStatisticsCollector();
        }

        public PauseCycle Pause { set; get; }

        public void DrawPath()
        {
            var vertex = graph.End;
            while (!vertex.IsStart)
            {
                var temp = vertex;
                vertex = vertex.ParentVertex;
                if (vertex.IsSimpleVertex)
                    vertex.MarkAsPath();
                (StatCollector as WeightedGraphSearchAlgoStatisticsCollector).AddLength(int.Parse(temp.Text));
                Pause(35);
            }
        }

        private IVertex GetChippestUnvisitedVertex()
        {
            neigbourQueue.RemoveAll(vertex => vertex.IsVisited);
            neigbourQueue.Sort((vertex1, vertex2) => vertex1.Value.CompareTo(vertex2.Value));
            return neigbourQueue.Any() ? neigbourQueue.First() : null;
        }

        protected virtual double GetPathValue(IVertex neighbour, IVertex vertex)
        {
            return int.Parse(neighbour.Text) + vertex.Value;
        }

        private void MakeWaves(IVertex vertex)
        {
            if (vertex is null)
                return;
            var neighbours = vertex.Neighbours;
            foreach(var neighbour in neighbours)
            {
                if (!neighbour.IsVisited)
                    neigbourQueue.Add(neighbour);
                if (neighbour.Value > GetPathValue(neighbour, vertex))
                {                    
                    neighbour.Value = GetPathValue(neighbour, vertex);
                    neighbour.ParentVertex = vertex;
                }
            }
        }

        public bool FindDestionation()
        {
            if (graph.End == null)
                return false;
            StatCollector.BeginCollectStatistic();
            var currentVertex = graph.Start;
            currentVertex.IsVisited = true;
            currentVertex.Value = 0;
            do
            {
                MakeWaves(currentVertex);
                currentVertex = GetChippestUnvisitedVertex();
                if (currentVertex?.Value == double.PositiveInfinity
                    || currentVertex == null)
                    break;
                if (IsRightCellToVisit(currentVertex))
                    Visit(currentVertex);
                Pause(2);
            } while (!IsDestination(currentVertex));
            StatCollector.StopCollectStatistics();
            return graph.End.IsVisited;
        }

        private bool IsDestination(IVertex vertex)
        {
            if (vertex == null)
                return false;
            if (vertex.IsObstacle)
                return false;
            return vertex.IsEnd && vertex.IsVisited;
        }

        private bool IsRightCellToVisit(IVertex vertex)
        {
            if (vertex is null)
                return false;
            if (vertex.IsObstacle)
                return false;
            else
                return !vertex.IsVisited;
        }

        private void Visit(IVertex vertex)
        {
            vertex.IsVisited = true;
            if (!vertex.IsEnd)
            {
                vertex.MarkAsCurrentlyLooked();
                Pause(8);
                vertex.MarkAsVisited();               
            }
            StatCollector.CellVisited();
        }

        public string GetStatistics()
        {
            return StatCollector.Statistics;
        }
    }
}
