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
    [MediumPriority]
    internal sealed class AStarLeeAlgorithmMenuItem(
        IReadOnlyDictionary<string, IHeuristic> heuristics,
        IMessenger messenger,
        IInput<int> intInput)
        : AlgorithmInputMenuItem(messenger, null, heuristics, intInput)
    {
        protected override string LanguageKey => AlgorithmNames.AStarLee;

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new AStarLeeAlgorithmFactory(heuristics);
        }
    }
}
