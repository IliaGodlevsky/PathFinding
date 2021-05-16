using Algorithm.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace Algorithm.Realizations.StepRules
{
    public sealed class DiagonalExpensiveStepRule : IStepRule
    {
        public DiagonalExpensiveStepRule(IStepRule stepRule)
        {
            this.stepRule = stepRule;
        }

        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            var stepCost = stepRule.CalculateStepCost(neighbour, current);
            if (!current.IsCardinal(neighbour))
            {
                return stepCost * 2;
            }

            return stepCost;
        }

        private readonly IStepRule stepRule;
    }
}
