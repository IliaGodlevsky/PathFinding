using Pathfinding.Service.Interface;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Algorithms.StepRules
{
    public sealed class WalkStepRule : IStepRule
    {
        private readonly IStepRule stepRule;
        private readonly int walkStepCost;

        public WalkStepRule(IStepRule stepRule, int walkStepCost = 1)
        {
            this.stepRule = stepRule;
            this.walkStepCost = walkStepCost;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateStepCost(IPathfindingVertex neighbour, IPathfindingVertex current)
        {
            return stepRule.CalculateStepCost(neighbour, current) + walkStepCost;
        }
    }
}