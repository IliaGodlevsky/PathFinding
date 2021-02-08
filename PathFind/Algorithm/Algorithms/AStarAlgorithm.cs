using Algorithm.Extensions;
using Algorithm.Handlers;
using GraphLib.Interface;
using GraphLib.NullObjects;
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

        protected override void PrepareForPathfinding(IVertex start, IVertex end)
        {
            base.PrepareForPathfinding(start, end);
            HeuristicFunction = vertex => vertex.CalculateChebyshevDistanceTo(End);
        }
    }
}
