using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NullObject.Attributes;

namespace Algorithm.Realizations.StepRules
{
    [Null]
    public sealed class NullStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return default;
        }
    }
}
