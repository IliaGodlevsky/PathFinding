﻿using SearchAlgorythms.Algorithm;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Statistics;
using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.PathFindAlgorithm
{
    public class DeepPathFindAlgorithm : IPathFindAlgorithm
    {
        private readonly AbstractGraph graph;
        private Stack<IVertex> visitedVerticesStack = new Stack<IVertex>();
        private WeightedGraphSearchAlgoStatistics statCollector;

        public DeepPathFindAlgorithm(AbstractGraph graph)
        {
            statCollector = new WeightedGraphSearchAlgoStatistics();
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
            while (!IsDestination(currentVertex))
            {
                temp = currentVertex;
                currentVertex = GoNextVertex(currentVertex);
                if (IsRightCellToVisit(currentVertex))
                {
                    Visit(currentVertex);
                    currentVertex.ParentVertex = temp;
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
