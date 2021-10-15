using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Realizations.StepRules
{
    /// <summary>
    /// A step rule, that immitates a process
    /// of wasting energy during the pathfinding process.
    /// This class can't be inherited
    /// </summary>
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