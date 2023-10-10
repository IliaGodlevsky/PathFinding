using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Runtime.CompilerServices;

namespace Pathfinding.AlgorithmLib.Core.Realizations.StepRules
{
    public sealed class LandscapeStepRule : IStepRule
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            var currentCost = current.Neighbours[neighbour];
            var neighbourCost = neighbour.Neighbours[current];
            return Math.Abs(neighbourCost.CurrentCost - currentCost.CurrentCost);
        }
    }
}