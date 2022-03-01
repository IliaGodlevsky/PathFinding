using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Realizations.StepRules
{
    public sealed class WalkStepRule : IStepRule
    {
        public WalkStepRule(IStepRule stepRule, int walkStepCost = 1)
        {
            this.stepRule = stepRule;
            this.walkStepCost = walkStepCost;
        }

        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return stepRule.CalculateStepCost(neighbour, current) + walkStepCost;
        }

        private readonly IStepRule stepRule;
        private readonly int walkStepCost;
    }
}