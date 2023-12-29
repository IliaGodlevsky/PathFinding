using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.App.Console.Model.Notes;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighPriority]
    internal sealed class IDAStarAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        private readonly InclusiveValueRange<int> spreadRange;

        private int Spread { get; set; }

        protected override string LanguageKey { get; } = nameof(Languages.IDAStarAlgorithm);

        public IDAStarAlgorithmMenuItem(
            IReadOnlyDictionary<string, IStepRule> stepRules,
            IReadOnlyDictionary<string, IHeuristic> heuristics,
            IInput<int> input,
            IMessenger messenger)
            : base(messenger, stepRules, heuristics, input)
        {
            spreadRange = new(8, 1);
        }

        protected override IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics)
        {
            return new IDAStarAlgorithmFactory(stepRule, heuristics, Spread);
        }

        protected override AlgorithmInfo GetAlgorithm()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                string message = string.Format(Languages.SpreadLevelMsg, spreadRange);
                Spread = intInput.Input(message, spreadRange);
            }
            return base.GetAlgorithm();
        }

        protected override Statistics GetStatistics(string heusristic, string stepRule)
        {
            var stats = base.GetStatistics(heusristic, stepRule);
            stats.Spread = Spread;
            return stats;
        }
    }
}
