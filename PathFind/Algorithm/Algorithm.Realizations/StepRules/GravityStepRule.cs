using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;
using System.ComponentModel;

namespace Algorithm.Realizations.StepRules
{
    [Description("Gravity step rule")]
    public sealed class DensityStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            var cost = current.Cost.CurrentCost - neighbour.Cost.CurrentCost;
            return cost < 0 ? Math.Abs(cost * 2) : Math.Round((double)cost / 2, 0);
        }
    }
}