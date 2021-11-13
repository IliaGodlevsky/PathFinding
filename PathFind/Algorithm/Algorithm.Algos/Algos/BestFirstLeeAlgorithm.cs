﻿using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
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
            heuristics = new Costs();
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
                    .OrderBy(heuristics.GetCost)
                    .ToQueue();

                return base.NextVertex;
            }
        }

        protected override void Reset()
        {
            base.Reset();
            heuristics.Clear();
        }

        protected override void Reevaluate(IVertex vertex, double value)
        {
            base.Reevaluate(vertex, value);
            double result = CalculateHeuristic(CurrentVertex);
            heuristics.Reevaluate(vertex, value + result);
        }

        protected override void PrepareForLocalPathfinding()
        {
            base.PrepareForLocalPathfinding();
            double value = CalculateHeuristic(CurrentEndPoints.Source);
            heuristics.Reevaluate(CurrentEndPoints.Source, value);
        }

        private double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentEndPoints.Target);
        }

        private readonly ICosts heuristics;
        private readonly IHeuristic heuristic;
    }
}