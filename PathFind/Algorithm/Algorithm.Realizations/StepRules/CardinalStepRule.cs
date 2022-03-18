using Algorithm.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Realizations.StepRules
{
    public sealed class CardinalStepRule : IStepRule
    {
        private readonly double stepCostIncreaseFactor;
        private readonly IStepRule stepRule;

        public CardinalStepRule(IStepRule stepRule, double stepCostIncreaseFactor = 1.5)
        {
            this.stepRule = stepRule;
            this.stepCostIncreaseFactor = stepCostIncreaseFactor;
        }

        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            if (current.IsCardinal(neighbour))
            {
                return stepRule.CalculateStepCost(neighbour, current);
            }

            double cost = stepRule.CalculateStepCost(neighbour, current);
            return Math.Round(stepCostIncreaseFactor * cost);
        }
    }
}