using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Realizations.StepRules
{
    [Description("Walk step rule")]
    public sealed class WalkStepRule : IStepRule
    {
        public WalkStepRule(IStepRule stepRule)
        {
            this.stepRule = stepRule;
        }

        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return stepRule.CalculateStepCost(neighbour, current) + 1;
        }

        private readonly IStepRule stepRule;
    }
}