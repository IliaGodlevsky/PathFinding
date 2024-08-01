using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Factories;
using Pathfinding.Service.Interface;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighPriority]
    internal sealed class IDAStarAlgorithmMenuItem(
        IReadOnlyDictionary<string, IStepRule> stepRules,
        IReadOnlyDictionary<string, IHeuristic> heuristics,
        IInput<int> input,
        IMessenger messenger)
        : AlgorithmInputMenuItem(messenger, stepRules, heuristics, input)
    {
        private static readonly InclusiveValueRange<int> SpreadRange = new(8, 1);

        private int Spread { get; set; }

        protected override string LanguageKey { get; } = AlgorithmNames.IDAStar;

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new IDAStarAlgorithmFactory(stepRule, heuristics, Spread);
        }

        protected override AlgorithmInfo GetAlgorithm()
        {
            var info = base.GetAlgorithm();
            using (Cursor.UseCurrentPositionWithClean())
            {
                string message = string.Format(Languages.SpreadLevelMsg, SpreadRange);
                Spread = intInput.Input(message, SpreadRange);
            }
            return new(info.Factory, info.AlgorithmId, info.StepRule, info.Heuristics, Spread);
        }
    }
}
