using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Realizations.StepRules
{
    public sealed class RatedStepRule : IStepRule
    {
        public RatedStepRule(IStepRule stepRule, int rate = 2)
        {
            this.stepRule = stepRule;
            this.rate = rate;
        }

        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return Math.Pow(stepRule.CalculateStepCost(neighbour, current), rate);
        }

        private readonly IStepRule stepRule;
        private readonly int rate;
    }
}