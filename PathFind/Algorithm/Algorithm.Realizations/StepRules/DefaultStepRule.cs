using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace Algorithm.Realizations.StepRules
{
    public sealed class DefaultStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return neighbour.Cost.CurrentCost;
        }
    }
}