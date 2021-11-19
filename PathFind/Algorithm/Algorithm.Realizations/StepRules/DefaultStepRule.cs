using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Realizations.StepRules
{
    /// <summary>
    /// A step rule, that says, that 
    /// next step is equal to
    /// neighbour vertex cost.
    /// This class can't inherited
    /// </summary>
    public sealed class DefaultStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return neighbour.Cost.CurrentCost;
        }
    }
}