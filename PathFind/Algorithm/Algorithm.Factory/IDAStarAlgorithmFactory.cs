﻿using Algorithm.Algos.Algos;
using Algorithm.Base;
using Algorithm.Factory.Attrbiutes;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Factory
{
    [WaveGroup(3)]
    [Description("IDA* algorithm")]
    public sealed class IDAStarAlgorithmFactory : IAlgorithmFactory
    {
        public IDAStarAlgorithmFactory(IStepRule stepRule, IHeuristic heuristic)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
        }

        public IDAStarAlgorithmFactory(IStepRule stepRule) : this(stepRule, new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithmFactory(IHeuristic heuristic) : this(new DefaultStepRule(), heuristic)
        {

        }

        public IDAStarAlgorithmFactory() : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public PathfindingAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            return new IDAStarAlgorithm(graph, endPoints, stepRule, heuristic);
        }

        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;
    }
}