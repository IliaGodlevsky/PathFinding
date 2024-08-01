using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Factories;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighestPriority]
    internal sealed class DijkstraAlgorithmMenuItem(
        IReadOnlyDictionary<string, IStepRule> stepRules,
        IInput<int> input,
        IMessenger messenger)
        : AlgorithmInputMenuItem(messenger, stepRules, null, input)
    {
        protected override string LanguageKey => AlgorithmNames.Dijkstra;

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new DijkstraAlgorithmFactory(stepRule);
        }
    }
}
