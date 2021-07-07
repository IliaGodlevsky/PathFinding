using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Realizations.StepRules
{
    /// <summary>
    /// A rule that calculates the 
    /// number of cost units required 
    /// to equalize the cost of two vertices.
    /// This class can't be inherited
    /// </summary>
    public sealed class LandscapeStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return Math.Abs(neighbour.Cost.CurrentCost - current.Cost.CurrentCost);
        }
    }
}