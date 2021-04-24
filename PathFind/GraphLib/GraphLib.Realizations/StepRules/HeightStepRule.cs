using GraphLib.Interfaces;
using System;

namespace GraphLib.Realizations.StepRules
{
    public sealed class HeightStepRule : IStepRule
    {
        public int StepCost(IVertex neighbour, IVertex current)
        {
            return Math.Abs(neighbour.Cost.CurrentCost - current.Cost.CurrentCost) + 1;
        }
    }
}