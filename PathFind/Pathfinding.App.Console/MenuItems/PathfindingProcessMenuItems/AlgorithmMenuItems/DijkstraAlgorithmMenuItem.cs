using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.App.Console.Model.Notes;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighestPriority]
    internal sealed class DijkstraAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        private readonly IReadOnlyDictionary<string, IStepRule> stepRules;

        public DijkstraAlgorithmMenuItem(IReadOnlyDictionary<string, IStepRule> stepRules,
            IInput<int> input, IMessenger messenger)
            : base(messenger, input)
        {
            this.stepRules = stepRules;
        }

        public override string ToString()
        {
            return Languages.DijkstraAlgorithm;
        }

        protected override (IAlgorithmFactory<PathfindingProcess> Algorithm, Statistics Statistics) GetAlgorithm()
        {
            var stepRule = InputItem(stepRules, Languages.ChooseStepRuleMsg);
            var statistics = new Statistics()
            {
                Algorithm = nameof(Languages.DijkstraAlgorithm),
                ResultStatus = nameof(Languages.Started),
                StepRule = stepRule.Key
            };
            var factory = new DijkstraAlgorithmFactory(stepRule.Value);
            return (factory, statistics);
        }
    }
}
