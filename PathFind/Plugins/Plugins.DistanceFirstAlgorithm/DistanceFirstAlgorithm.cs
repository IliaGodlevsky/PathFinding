using Algorithm.Base;
using Algorithm.Extensions;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;

namespace Plugins.DistanceFirstAlgorithm
{
    [ClassName("Distance-first algorithm")]
    public class DistanceFirstAlgorithm : BaseGreedyAlgorithm
    {
        public DistanceFirstAlgorithm(IGraph graph, IEndPoints endPoints)
            : base(graph, endPoints)
        {

        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return vertex.CalculateChebyshevDistanceTo(endPoints.End);
        }
    }
}
