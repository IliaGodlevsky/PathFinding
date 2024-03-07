using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.DAL;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
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
            using (Cursor.UseCurrentPositionWithClean())
            {
                string message = string.Format(Languages.SpreadLevelMsg, SpreadRange);
                Spread = intInput.Input(message, SpreadRange);
            }
            return base.GetAlgorithm();
        }

        protected override RunStatisticsDto GetStatistics(string heusristic, string stepRule)
        {
            return base.GetStatistics(heusristic, stepRule) with { Spread = Spread };
        }
    }
}
