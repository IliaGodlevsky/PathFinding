using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using System;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Algorithms.StepRules
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            double cost = stepRule.CalculateStepCost(neighbour, current);
            return current.Position.IsCardinal(neighbour.Position)
                ? cost
                : Math.Round(stepCostIncreaseFactor * cost);
        }
    }
}