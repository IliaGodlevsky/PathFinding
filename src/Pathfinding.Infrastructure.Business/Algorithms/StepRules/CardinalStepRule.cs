using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Algorithms.StepRules
{
    public sealed class CardinalStepRule : IStepRule
    {
        private readonly static double DiagonalStepFactor = Math.Sqrt(2);

        private readonly IStepRule stepRule;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CardinalStepRule(IStepRule stepRule)
        {
            this.stepRule = stepRule;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateStepCost(IPathfindingVertex neighbour, IPathfindingVertex current)
        {
            double cost = stepRule.CalculateStepCost(neighbour, current);
            bool isCardinal = current.Position.IsCardinal(neighbour.Position);
            return isCardinal ? cost : Math.Round(DiagonalStepFactor * cost);
        }
    }
}