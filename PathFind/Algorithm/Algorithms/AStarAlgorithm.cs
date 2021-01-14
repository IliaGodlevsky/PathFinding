using Algorithm.Handlers;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.ComponentModel;

namespace Algorithm.Algorithms
{
    [Description("A* algorithm")]
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public HeuristicHandler HeuristicFunction { protected get; set; }

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
