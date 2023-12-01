using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    [LowPriority]
    internal sealed class CostGreedyAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        protected override string LanguageKey { get; } = nameof(Languages.CostGreedyAlgorithm);

        public CostGreedyAlgorithmMenuItem(IReadOnlyDictionary<string, IStepRule> stepRules,
            IMessenger messenger, IInput<int> intInput)
            : base(messenger, stepRules, null, intInput)
        {

        }

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new CostGreedyAlgorithmFactory(stepRule);
        }
    }
}
