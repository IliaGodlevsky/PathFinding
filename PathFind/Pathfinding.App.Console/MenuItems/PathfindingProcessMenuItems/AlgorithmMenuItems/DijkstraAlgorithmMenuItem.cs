using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighestPriority]
    internal sealed class DijkstraAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        protected override string LanguageKey { get; } = nameof(Languages.DijkstraAlgorithm);

        public DijkstraAlgorithmMenuItem(IReadOnlyDictionary<string, IStepRule> stepRules,
            IInput<int> input, IMessenger messenger)
            : base(messenger, stepRules, null, input)
        {

        }

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new DijkstraAlgorithmFactory(stepRule);
        }
    }
}
