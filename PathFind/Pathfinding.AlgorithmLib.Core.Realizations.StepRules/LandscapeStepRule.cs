using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.AlgorithmLib.Core.Realizations.StepRules
{
    public sealed class LandscapeStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return Math.Abs(neighbour.Cost.CurrentCost - current.Cost.CurrentCost);
        }
    }
}