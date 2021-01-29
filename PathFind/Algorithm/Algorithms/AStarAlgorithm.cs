using Algorithm.Extensions;
using Algorithm.Handlers;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.ComponentModel;

namespace Algorithm.Algorithms
{
    [Description("A* algorithm")]
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public HeuristicHandler HeuristicFunction { get; set; }

        public AStarAlgorithm() : this(new NullGraph())
        {

        }

        public AStarAlgorithm(IGraph graph) : base(graph)
        {
            HeuristicFunction = vertex => vertex.CalculateChebyshevDistanceTo(Graph.End);
        }

        public override void Reset()
        {
            base.Reset();
            HeuristicFunction = null;
        }

        protected override double GetVertexRelaxedCost(IVertex neighbour)
        {
            return base.GetVertexRelaxedCost(neighbour) + HeuristicFunction(CurrentVertex);
        }
    }
}
