using Algorithm.Base;
using Algorithm.Extensions;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;

namespace Plugins.DepthFirstAlgorithm
{
    [ClassName("Depth first algorithm")]
    public sealed class DepthFirstAlgorithm : BaseGreedyAlgorithm
    {
        public DepthFirstAlgorithm(IGraph graph, IEndPoints endPoints)
            : base(graph, endPoints)
        {

        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return vertex.CalculateChebyshevDistanceTo(endPoints.Start);
        }
    }
}
