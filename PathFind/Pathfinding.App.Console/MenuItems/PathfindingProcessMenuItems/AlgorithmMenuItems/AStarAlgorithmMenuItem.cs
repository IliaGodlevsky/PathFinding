using GalaSoft.MvvmLight.Messaging;
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
    [HighPriority]
    internal sealed class AStarAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        protected override string LanguageKey { get; } = nameof(Languages.AStarAlgorithm);

        public AStarAlgorithmMenuItem(
            IReadOnlyDictionary<string, IStepRule> stepRules,
            IReadOnlyDictionary<string, IHeuristic> heuristics,
            IInput<int> input,
            IMessenger messenger)
            : base(messenger, stepRules, heuristics, input)
        {
        }

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new AStarAlgorithmFactory(stepRule, heuristics);
        }
    }
}
