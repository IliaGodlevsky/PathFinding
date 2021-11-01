using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Common.Extensions.EnumerableExtensions;
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
                    .OrderBy(accumulatedCosts.GetAccumulatedCost)
                    .ToQueue();

                return base.NextVertex;
            }
        }

        protected override double CreateWave()
        {
            return base.CreateWave() + heuristic.Calculate(CurrentVertex, CurrentEndPoints.Target);
        }

        private readonly IHeuristic heuristic;
    }
}
