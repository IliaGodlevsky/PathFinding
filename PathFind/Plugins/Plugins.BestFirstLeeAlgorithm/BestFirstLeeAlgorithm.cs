using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.HeuristicFunctions;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;
using System.Linq;

namespace Plugins.BestFirstLeeAlgorithm
{
    [ClassName("Lee algorithm (heuristic)")]
    public class BestFirstLeeAlgorithm : LeeAlgorithm.LeeAlgorithm
    {
        public BestFirstLeeAlgorithm(IGraph graph,
            IEndPoints endPoints, IHeuristicFunction function)
            : base(graph, endPoints)
        {
            heuristicFunction = function;
        }

        public BestFirstLeeAlgorithm(IGraph graph,
            IEndPoints endPoints)
            : this(graph, endPoints, new ChebyshevDistance())
        {

        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue.OrderBy(accumulatedCosts.GetAccumulatedCost).ToQueue();
                return base.NextVertex;
            }
        }

        protected virtual double CalculateHeuristic(IVertex vertex)
        {
            return heuristicFunction.Calculate(vertex, endPoints.End);
        }

        protected override double CreateWave()
        {
            return base.CreateWave() + CalculateHeuristic(CurrentVertex);
        }

        private readonly IHeuristicFunction heuristicFunction;
    }
}
