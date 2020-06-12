﻿using GraphLibrary.Algorithm;
using GraphLibrary.Constants;
using GraphLibrary.Graph;
using GraphLibrary.Statistics;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.PathFindAlgorithm
{
    public class DeepPathFindAlgorithm : IPathFindAlgorithm
    {
        private readonly AbstractGraph graph;
        private Stack<IVertex> visitedVerticesStack = new Stack<IVertex>();
       public AbstractStatisticsCollector StatCollector { get; set; }

        public DeepPathFindAlgorithm(AbstractGraph graph)
        {
            StatCollector = new WeightedGraphSearchAlgoStatisticsCollector();
            this.graph = graph;
        }

        protected virtual IVertex GoNextVertex(IVertex vertex)
        {
            return vertex.Neighbours.Find(vert => !vert.IsVisited);
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
                Pause(Const.PATH_DRAW_PAUSE_MILLISECONDS);
            }
        }

        public bool FindDestionation()
        {
            if (graph.End == null)
                return false;
            StatCollector.BeginCollectStatistic();
            var currentVertex = graph.Start;
            Visit(currentVertex);
            while (!IsDestination(currentVertex))
            {
                IVertex temp = currentVertex;
                currentVertex = GoNextVertex(currentVertex);
                if (IsRightCellToVisit(currentVertex))
                {
                    Visit(currentVertex);
                    currentVertex.ParentVertex = temp;
                }
                else
                    currentVertex = visitedVerticesStack.Pop();
                Pause(Const.FIND_PROCESS_PAUSE_MILLISECONDS);
            }
            StatCollector.StopCollectStatistics();
            return graph.End.IsVisited;
        }

        private bool IsDestination(IVertex vertex)
        {
            return vertex.IsEnd && vertex.IsVisited
                || (!visitedVerticesStack.Any() && vertex.Neighbours.Find(v => !v.IsVisited) == null);
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
                Pause(Const.VISIT_PAUSE_MILLISECONDS);
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
