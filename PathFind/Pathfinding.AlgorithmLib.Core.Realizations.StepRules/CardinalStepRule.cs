using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System;

namespace Pathfinding.AlgorithmLib.Core.Realizations.StepRules
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
            double cost = stepRule.CalculateStepCost(neighbour, current);
            return current.IsCardinal(neighbour) 
                ? cost 
                : Math.Round(stepCostIncreaseFactor * cost);
        }
    }
}