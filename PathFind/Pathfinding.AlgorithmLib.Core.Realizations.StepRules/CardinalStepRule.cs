using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System;
using System.Runtime.CompilerServices;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            double cost = stepRule.CalculateStepCost(neighbour, current);
            return current.Position.IsCardinal(neighbour.Position)
                ? cost
                : Math.Round(stepCostIncreaseFactor * cost);
        }

        public override string ToString()
        {
            return "Cardinal step rule";
        }
    }
}