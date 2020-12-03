using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;
using System.ComponentModel;

namespace Algorithm.Algorithms
{
    [Description("A* algorithm")]
    internal class AStarAlgorithm : DijkstraAlgorithm
    {
        public Func<IVertex, double> HeuristicFunction { protected get; set; }

        public AStarAlgorithm(IGraph graph) : base(graph)
        {
            HeuristicFunction = vertex => vertex.GetChebyshevDistanceTo(graph.End);
        }

        protected override double GetVertexRelaxedCost(IVertex neighbour)
        {
            return base.GetVertexRelaxedCost(neighbour) + HeuristicFunction(CurrentVertex);
        }
    }
}
