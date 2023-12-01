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
    [MediumPriority]
    internal sealed class AStarLeeAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        protected override string LanguageKey { get; } = nameof(Languages.AStarLeeAlgorithm);

        public AStarLeeAlgorithmMenuItem(IReadOnlyDictionary<string, IHeuristic> heuristics,
            IMessenger messenger, IInput<int> intInput)
            : base(messenger, null, heuristics, intInput)
        {

        }

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new AStarLeeAlgorithmFactory(heuristics);
        }
    }
}
