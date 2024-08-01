using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Factories;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    [LowPriority]
    internal sealed class CostGreedyAlgorithmMenuItem(
        IReadOnlyDictionary<string, IStepRule> stepRules,
        IMessenger messenger,
        IInput<int> intInput)
        : AlgorithmInputMenuItem(messenger, stepRules, null, intInput)
    {
        protected override string LanguageKey => AlgorithmNames.Cost;

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new CostGreedyAlgorithmFactory(stepRule);
        }
    }
}
