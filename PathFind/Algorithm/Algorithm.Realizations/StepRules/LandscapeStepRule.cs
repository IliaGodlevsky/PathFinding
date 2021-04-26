using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;
using System.ComponentModel;

namespace Algorithm.Realizations.StepRules
{
    [Description("Landscape step rule")]
    public sealed class LandscapeStepRule : IStepRule
    {
        public double CountStepCost(IVertex neighbour, IVertex current)
        {
            return Math.Abs(neighbour.Cost.CurrentCost - current.Cost.CurrentCost);
        }
    }
}