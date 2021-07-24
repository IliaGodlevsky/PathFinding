using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using GraphLib.Interfaces;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    public class BestFirstLeeAlgorithm : LeeAlgorithm
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
            return base.CreateWave() + heuristic.Calculate(CurrentVertex, endPoints.Target);
        }

        private readonly IHeuristic heuristic;
    }
}
