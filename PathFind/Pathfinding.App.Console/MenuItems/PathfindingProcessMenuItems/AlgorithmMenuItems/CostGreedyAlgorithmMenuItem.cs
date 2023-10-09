using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.Notes;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    [MediumPriority]
    internal sealed class CostGreedyAlgorithmMenuItem : AlgorithmInputMenuItem
    {
        private readonly IReadOnlyDictionary<string, IStepRule> stepRules;

        public CostGreedyAlgorithmMenuItem(
            IReadOnlyDictionary<string, IStepRule> stepRules,
            IMessenger messenger, 
            IInput<int> intInput) 
            : base(messenger, intInput)
        {
            this.stepRules = stepRules;
        }

        protected override (IAlgorithmFactory<PathfindingProcess> Algorithm, Statistics Statistics) GetAlgorithm()
        {
            var stepRule = InputItem(stepRules, Languages.ChooseStepRuleMsg);
            var statistics = new Statistics(nameof(Languages.CostGreedyAlgorithm))
            {
                ResultStatus = nameof(Languages.Started),
                StepRule = stepRule.Key
            };
            var factory = new CostGreedyAlgorithmFactory(stepRule.Value);
            return (factory, statistics);
        }

        public override string ToString()
        {
            return Languages.CostGreedyAlgorithm;
        }
    }
}
