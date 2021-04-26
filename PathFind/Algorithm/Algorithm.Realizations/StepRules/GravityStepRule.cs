using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;
using System.ComponentModel;

namespace Algorithm.Realizations.StepRules
{
    [Description("Gravity step rule")]
    public sealed class GravityStepRule : IStepRule
    {
        public double CountStepCost(IVertex neighbour, IVertex current)
        {
            var cost = Math.Abs(current.Cost.CurrentCost - neighbour.Cost.CurrentCost);
            if (current.Cost.CurrentCost < neighbour.Cost.CurrentCost)
            {
                return cost * 2;
            }
            if (current.Cost.CurrentCost == neighbour.Cost.CurrentCost)
            {
                return cost;
            }

            return Math.Round((double) cost / 2, 0);
        }
    }
}