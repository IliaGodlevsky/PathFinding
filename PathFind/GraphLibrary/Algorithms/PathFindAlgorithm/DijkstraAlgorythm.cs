using GraphLibrary.Common.Extensions;
using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Graph;
using GraphLibrary.PauseMaker;
using GraphLibrary.Statistics;
using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Algorithm
{
    /// <summary>
    /// Finds the chippest path to destination top. 
    /// </summary>    
    public class DijkstraAlgorithm : IPathFindAlgorithm
    {        
        public IStatisticsCollector StatCollector { get; set; }
        public Func<IVertex, IVertex, double> RelaxFunction { get; set; }
        public AbstractGraph Graph { get; set; }
        public IPauseProvider Pauser { get; set; }

        public DijkstraAlgorithm()
        {
            neigbourQueue = new List<IVertex>();
            StatCollector = new StatisticsCollector();
            RelaxFunction = (neighbour, vertex) => neighbour.Cost + vertex.AccumulatedCost;
        }

        public void DrawPath() => this.DrawPath(vertex => vertex.ParentVertex);

        public void FindDestionation()
        {
            if (this.IsRightGraphSettings())
            {
                SetAccumulatedCostToInfinity();
                StatCollector.StartCollect();
                var currentVertex = Graph.Start;
                currentVertex.IsVisited = true;
                currentVertex.AccumulatedCost = 0;
                do
                {
                    ExtractNeighbours(currentVertex);
                    SpreadRelaxWave(currentVertex);
                    currentVertex = GetChippestUnvisitedVertex();
                    if (!IsValidVertex(currentVertex))
                        break;
                    if (!currentVertex.IsVisited)
                        this.VisitVertex(currentVertex);
                } while (!IsDestination(currentVertex));
                StatCollector.StopCollect();
            }
        }

        private void SetAccumulatedCostToInfinity()
        {
            IVertex SetValueToInfinity(IVertex vertex)
            {
                if (!vertex.IsObstacle)
                    vertex.AccumulatedCost = double.PositiveInfinity;
                return vertex;
            }
            Graph.GetArray().Apply(SetValueToInfinity);
        }

        private void ExtractNeighbours(IVertex vertex)
        {
            foreach (var neighbour in vertex.Neighbours)
            {
                if (!neighbour.IsVisited)
                    neigbourQueue.Add(neighbour);
            }
        }

        private void SpreadRelaxWave(IVertex vertex)
        {
            foreach (var neighbour in vertex.Neighbours)
            {
                var relaxFunctionResult = RelaxFunction(neighbour, vertex);
                if (neighbour.AccumulatedCost > relaxFunctionResult)
                {
                    neighbour.AccumulatedCost = relaxFunctionResult;
                    neighbour.ParentVertex = vertex;
                }
            }
        }

        private IVertex GetChippestUnvisitedVertex()
        {
            neigbourQueue.RemoveAll(vertex => vertex.IsVisited);
            neigbourQueue.Sort((vertex1, vertex2) => 
                vertex1.AccumulatedCost.CompareTo(vertex2.AccumulatedCost));
            return neigbourQueue.FirstOrDefault();
        }

        private bool IsValidVertex(IVertex vertex)
        {
            if (vertex == null)
                return false;
            return vertex.AccumulatedCost != double.PositiveInfinity;
        }

        private bool IsDestination(IVertex vertex)
        {
            return vertex.IsEnd && vertex.IsVisited
                || Graph.End == null;
        }

        private readonly List<IVertex> neigbourQueue;
    }
}
