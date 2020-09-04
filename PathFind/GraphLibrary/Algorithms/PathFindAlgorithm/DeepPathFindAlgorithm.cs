using GraphLibrary.Algorithm;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.PauseMaker;
using GraphLibrary.Statistics;
using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.PathFindAlgorithm
{
    public class DeepPathFindAlgorithm : IPathFindAlgorithm
    {        
        public IStatisticsCollector StatCollector { get; set; }
        public Graph Graph { get; set; }
        public IPauseProvider Pauser { get; set; }

        public DeepPathFindAlgorithm()
        {
            visitedVerticesStack = new Stack<IVertex>();
            StatCollector = new StatisticsCollector();
        }

        public void FindDestionation()
        {

                StatCollector.StartCollect();
                var currentVertex = Graph.Start;
                this.VisitVertex(currentVertex);
                while (!IsDestination(currentVertex))
                {
                    var temp = currentVertex;
                    currentVertex = GoNextVertex(currentVertex);
                    if (IsRightVertexToVisit(currentVertex))
                    {
                        this.VisitVertex(currentVertex);
                        visitedVerticesStack.Push(currentVertex);
                        currentVertex.ParentVertex = temp;
                    }
                    else
                        currentVertex = visitedVerticesStack.Pop();
                }
                StatCollector.StopCollect();
            
        }

        protected virtual IVertex GoNextVertex(IVertex vertex)
        {
            return vertex.GetUnvisitedNeighbours().Shuffle().FirstOrDefault();
        }

        private bool IsDestination(IVertex vertex)
        {
            return vertex.IsEnd || !visitedVerticesStack.Any()
                && !vertex.GetUnvisitedNeighbours().Any() || Graph.End == null;
        }

        private bool IsRightVertexToVisit(IVertex vertex)
        {
            return vertex != null || vertex?.IsObstacle == false;
        }

        private readonly Stack<IVertex> visitedVerticesStack;
    }
}
