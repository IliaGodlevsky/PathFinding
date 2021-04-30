using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;
using System.Linq;

namespace Plugins.BestFirstLeeAlgorithm
{
    [ClassName("Lee algorithm (heuristic)")]
    public class BestFirstLeeAlgorithm : LeeAlgorithm.LeeAlgorithm
    {
        public BestFirstLeeAlgorithm(IGraph graph, IEndPoints endPoints, IHeuristic function)
            : base(graph, endPoints)
        {
            heuristic = function;
        }

        public BestFirstLeeAlgorithm(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new ManhattanDistance())
        {

        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .OrderBy(accumulatedCosts.GetAccumulatedCost)
                    .ToQueue();

                return base.NextVertex;
            }
        }

        protected override double CreateWave()
        {
            return base.CreateWave() + heuristic.Calculate(CurrentVertex, endPoints.End);
        }

        private readonly IHeuristic heuristic;
    }
}
