using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Core.Realizations.StepRules
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

        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return stepRule.CalculateStepCost(neighbour, current) + walkStepCost;
        }
    }
}