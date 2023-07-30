﻿using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.Notes;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    [HighPriority]
    internal sealed class AStarAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        private readonly IReadOnlyDictionary<StepRules, IStepRule> stepRules;
        private readonly IReadOnlyDictionary<Heuristics, IHeuristic> heuristics;

        public AStarAlgorithmMenuItem(
            IReadOnlyDictionary<StepRules, IStepRule> stepRules,
            IReadOnlyDictionary<Heuristics, IHeuristic> heuristics,
            IInput<int> input,
            IMessenger messenger)
            : base(messenger, input)
        {
            this.stepRules = stepRules;
            this.heuristics = heuristics;
        }

        public override string ToString()
        {
            return Languages.AStarAlgorithm;
        }

        protected override (IAlgorithmFactory<PathfindingProcess> Algorithm, Statistics Statistics) GetAlgorithm()
        {
            var stepRule = InputItem(stepRules, Languages.ChooseStepRuleMsg);
            var heuristic = InputItem(heuristics, Languages.ChooseHeuristicMsg);
            var statistics = new Statistics
            {
                Algorithm = Algorithms.AStarAlgorithm,
                ResultStatus = AlgorithmStatus.Started,
                StepRule = stepRule.Key,
                Heuristics = heuristic.Key
            };
            var factory = new AStarAlgorithmFactory(stepRule.Value, heuristic.Value);
            return (factory, statistics);
        }
    }
}
