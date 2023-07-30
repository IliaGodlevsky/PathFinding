using GalaSoft.MvvmLight.Messaging;
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
        private readonly IReadOnlyDictionary<StepRules, IStepRule> stepRules;
        private readonly IReadOnlyDictionary<Heuristics, IHeuristic> heuristics;
        private readonly InclusiveValueRange<int> spreadRange;

        public IDAStarAlgorithmMenuItem(
            IReadOnlyDictionary<StepRules, IStepRule> stepRules,
            IReadOnlyDictionary<Heuristics, IHeuristic> heuristics,
            IInput<int> input,
            IMessenger messenger)
            : base(messenger, input)
        {
            this.stepRules = stepRules;
            this.heuristics = heuristics;
            spreadRange = new(8, 1);
        }

        public override string ToString()
        {
            return Languages.IDAStarAlgorithm;
        }

        protected override (IAlgorithmFactory<PathfindingProcess> Algorithm, Statistics Statistics) GetAlgorithm()
        {
            var stepRule = InputItem(stepRules, Languages.ChooseStepRuleMsg);
            var heuristic = InputItem(heuristics, Languages.ChooseHeuristicMsg);
            using (Cursor.UseCurrentPositionWithClean())
            {
                string message = string.Format(Languages.SpreadLevelMsg, spreadRange);
                int spread = intInput.Input(message, spreadRange);
                var statistics = new Statistics
                {
                    Algorithm = Algorithms.IDAStarAlgorithm,
                    ResultStatus = AlgorithmStatus.Started,
                    StepRule = stepRule.Key,
                    Heuristics = heuristic.Key,
                    Spread = spread
                };
                var factory = new IDAStarAlgorithmFactory(stepRule.Value, heuristic.Value, spread);
                return (factory, statistics);
            }
        }
    }
}
