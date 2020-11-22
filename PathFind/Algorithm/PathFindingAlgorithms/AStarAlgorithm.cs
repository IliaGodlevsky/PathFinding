using Algorithm.Сalculations;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;
using System.ComponentModel;

namespace Algorithm.PathFindingAlgorithms
{
    [Description("A* algorithm")]
    internal class AStarAlgorithm : DijkstraAlgorithm
    {
        public Func<IVertex, double> HeuristicFunction { protected get; set; }

        public AStarAlgorithm(IGraph graph) : base(graph)
        {
            HeuristicFunction = vertex => DistanceCalculator.
                    CalculateChebyshevDistance(vertex, graph.End);
        }

        protected override double GetVertexRelaxedCost(IVertex neighbour, IVertex vertex)
        {
            return base.GetVertexRelaxedCost(neighbour, vertex) + HeuristicFunction(vertex);
        }
    }
}
