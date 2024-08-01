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
    internal sealed class DepthFirstAlgorithmMenuItem(IReadOnlyDictionary<string, IHeuristic> heuristics,
        IMessenger messenger,
        IInput<int> intInput)
        : AlgorithmInputMenuItem(messenger, null, heuristics, intInput)
    {
        protected override string LanguageKey => AlgorithmNames.Depth;

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new DepthFirstAlgorithmFactory(heuristics);
        }
    }
}
