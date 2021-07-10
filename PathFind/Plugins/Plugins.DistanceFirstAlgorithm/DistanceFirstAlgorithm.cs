using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;

namespace Plugins.DistanceFirstAlgorithm
{
    [ClassName("Distance-first algorithm")]
    public class DistanceFirstAlgorithm : GreedyAlgorithm
    {
        public DistanceFirstAlgorithm(IGraph graph,
            IEndPoints endPoints, IHeuristic heuristic)
            : base(graph, endPoints)
        {
            this.heuristic = heuristic;
        }

        public DistanceFirstAlgorithm(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new EuclidianDistance())
        {

        }

        protected override double GreedyHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, endPoints.Target);
        }

        private readonly IHeuristic heuristic;
    }
}
