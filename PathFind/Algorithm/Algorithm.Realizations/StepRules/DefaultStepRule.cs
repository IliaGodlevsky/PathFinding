using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.ComponentModel;

namespace Algorithm.Realizations.StepRules
{
    [Description("Default step rule")]

    public sealed class DefaultStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return neighbour.Cost.CurrentCost;
        }
    }
}