using System.Collections.Generic;
using System.Linq;
using SearchAlgorythms.Extensions.ListExtensions;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Statistics;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    /// <summary>
    /// Greedy algorithm. Each step looks for the chippest top and visit it
    /// </summary>
    public class GreedyAlgorithm : IPathFindAlgorithm
    {
        private readonly AbstractGraph graph;
        private Stack<IVertex> visitedVerticesStack = new Stack<IVertex>();
        private WeightedGraphSearchAlgoStatistics statCollector;

        public GreedyAlgorithm(AbstractGraph graph)
        {
            statCollector = new WeightedGraphSearchAlgoStatistics();
            this.graph = graph;
        }

        private IVertex GoChippestNeighbour(IVertex vertex)
        {
            var neighbours = vertex.Neighbours.Count(vert => vert.IsVisited) == 0 
                ? vertex.Neighbours : vertex.Neighbours.Where(vert => !vert.IsVisited).ToList();
            neighbours.Shuffle();
            if (neighbours.Any())
            {
                double min = neighbours.Min(vert => int.Parse(vert.Text));
                return neighbours.Find(vert => vert.Text == min.ToString());
            }
            return null;
        }

        public PauseCycle Pause { set; get; }

        public void DrawPath()
        {
            var vertex = graph.End;
            while (!vertex.IsStart)
            {
                var temp = vertex;
                vertex = vertex.ParentTop;
                if (vertex.IsSimpleVertex)
                    vertex.MarkAsPath();
                statCollector.AddLength(int.Parse(temp.Text));
                Pause(35);
            }
        }

        public bool FindDestionation()
        {
            if (graph.End == null)
                return false;
            statCollector.BeginCollectStatistic();
            var currentVertex = graph.Start;
            IVertex temp = null;
            Visit(currentVertex);
            while(!IsDestination(currentVertex))
            {
                temp = currentVertex;
                currentVertex = GoChippestNeighbour(currentVertex);
                if (IsRightCellToVisit(currentVertex))
                {
                    Visit(currentVertex);
                    currentVertex.ParentTop = temp;
                }
                else
                    currentVertex = visitedVerticesStack.Pop();
                Pause(2);
            }
            statCollector.StopCollectStatistics();
            return graph.End.IsVisited;
        }

        private bool IsDestination(IVertex vertex)
        {
            return vertex.IsEnd && vertex.IsVisited || !visitedVerticesStack.Any();
        }

        private bool IsRightCellToVisit(IVertex vertex)
        {
            if (vertex == null)
                return false;
            if (vertex.IsObstacle)
                return false;
            return true;
        }

        private void Visit(IVertex vertex)
        {
            vertex.IsVisited = true;
            visitedVerticesStack.Push(vertex);
            if (vertex.IsSimpleVertex)
            {
                vertex.MarkAsCurrentlyLooked();
                Pause(8);
                vertex.MarkAsVisited();
            }
            statCollector.CellVisited();
        }

        public string GetStatistics()
        {
            return statCollector.Statistics;
        }
    }
}
