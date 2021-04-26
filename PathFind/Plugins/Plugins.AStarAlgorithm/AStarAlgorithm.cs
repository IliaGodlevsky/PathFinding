using Algorithm.Interfaces;
using Algorithm.Realizations.HeuristicFunctions;
using Algorithm.Realizations.StepRules;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;
using Plugins.DijkstraALgorithm;

namespace Plugins.AStarAlgorithm
{
    [ClassName("A* algorithm")]
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public AStarAlgorithm(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public AStarAlgorithm(IGraph graph, IEndPoints endPoints, IStepRule stepRule, IHeuristicFunction function)
            : base(graph, endPoints, stepRule)
        {
            heuristicFunction = function;
        }

        public AStarAlgorithm(IGraph graph, IEndPoints endPoints, IHeuristicFunction function)
            : this(graph, endPoints, new DefaultStepRule(), function)
        {

        }

        public AStarAlgorithm(IGraph graph, IEndPoints endPoints, IStepRule stepRule)
            : this(graph, endPoints, stepRule, new ChebyshevDistance())
        {

        }

        protected override double GetVertexRelaxedCost(IVertex neighbour)
        {
            return base.GetVertexRelaxedCost(neighbour)
                   + heuristicFunction.Calculate(CurrentVertex, endPoints.End);
        }

        protected readonly IHeuristicFunction heuristicFunction;
    }
}
