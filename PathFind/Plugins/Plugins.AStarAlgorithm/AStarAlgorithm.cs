using Algorithm.Extensions;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;
using GraphLib.Realizations.StepRules;
using Plugins.DijkstraALgorithm;

namespace Plugins.AStarAlgorithm
{
    [ClassName("A* algorithm")]
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public AStarAlgorithm(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new DefaultStepRule())
        {

        }

        public AStarAlgorithm(IGraph graph, IEndPoints endPoints, IStepRule stepRule)
            : base(graph, endPoints, stepRule)
        {

        }

        protected virtual double CalculateHeuristic(IVertex vertex)
        {
            return vertex.CalculateChebyshevDistanceTo(endPoints.End);
        }

        protected override double GetVertexRelaxedCost(IVertex neighbour)
        {
            return base.GetVertexRelaxedCost(neighbour) + CalculateHeuristic(CurrentVertex);
        }
    }
}
