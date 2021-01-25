using Algorithm.Extensions;
using Algorithm.Handlers;
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
            HeuristicFunction = vertex => vertex.CalculateChebyshevDistanceTo(graph.End);
        }

        protected override double GetVertexRelaxedCost(IVertex neighbour)
        {
            return base.GetVertexRelaxedCost(neighbour) + HeuristicFunction(CurrentVertex);
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            HeuristicFunction = null;
        }
    }
}
