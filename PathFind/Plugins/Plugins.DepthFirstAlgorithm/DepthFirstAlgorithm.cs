using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;

namespace Plugins.DepthFirstAlgorithm
{
    [ClassName("Depth first algorithm")]
    public sealed class DepthFirstAlgorithm : BaseGreedyAlgorithm
    {
        public DepthFirstAlgorithm(IGraph graph, IEndPoints endPoints, IHeuristic heuristic)
            : base(graph, endPoints)
        {
            this.heuristic = heuristic;
        }

        public DepthFirstAlgorithm(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new ManhattanDistance())
        {

        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, endPoints.Start);
        }

        private readonly IHeuristic heuristic;
    }
}
