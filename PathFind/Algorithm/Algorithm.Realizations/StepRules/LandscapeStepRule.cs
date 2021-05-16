using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Realizations.StepRules
{
    public sealed class LandscapeStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return Math.Abs(neighbour.Cost.CurrentCost - current.Cost.CurrentCost);
        }
    }
}