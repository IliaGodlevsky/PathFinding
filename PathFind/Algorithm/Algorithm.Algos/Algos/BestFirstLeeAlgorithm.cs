using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using Interruptable.Interface;
using System;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    /// <summary>
    /// A version of <see cref="LeeAlgorithm"/> that seaches path
    /// with a heuristic function
    /// </summary>
    public class BestFirstLeeAlgorithm : LeeAlgorithm,
        IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BestFirstLeeAlgorithm"/>
        /// </summary>
        /// <param name="graph">A graph, where the cheapest path must be founded</param>
        /// <param name="endPoints">Vertices, between which the cheapest path must be founded</param>
        /// <param name="function">A function, that influences on the choosing the next vertex to move</param>
        public BestFirstLeeAlgorithm(IGraph graph, IIntermediateEndPoints endPoints, IHeuristic function)
            : base(graph, endPoints)
        {
            heuristic = function;
        }

        public BestFirstLeeAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
            : this(graph, endPoints, new ManhattanDistance())
        {

        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .OrderBy(accumulatedCostWithHeuristic.GetAccumulatedCost)
                    .ToQueue();

                return base.NextVertex;
            }
        }

        protected override void Reevaluate(IVertex vertex, double value)
        {
            base.Reevaluate(vertex, value);
            value += heuristic.Calculate(CurrentVertex, CurrentEndPoints.Target);
            accumulatedCostWithHeuristic.Reevaluate(vertex, value);
        }

        protected override void PrepareForLocalPathfinding()
        {
            base.PrepareForLocalPathfinding();
            var vertices = graph.GetNotObstacles().Without(CurrentEndPoints.Source);
            accumulatedCostWithHeuristic = new AccumulatedCosts(vertices, double.PositiveInfinity);
            double value = heuristic.Calculate(CurrentEndPoints.Source, CurrentEndPoints.Target);
            accumulatedCostWithHeuristic.Reevaluate(CurrentEndPoints.Source, value);
        }

        protected IAccumulatedCosts accumulatedCostWithHeuristic;
        private readonly IHeuristic heuristic;
    }
}
