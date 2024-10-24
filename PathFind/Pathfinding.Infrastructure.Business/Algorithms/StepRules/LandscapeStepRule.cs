using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using System;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Algorithms.StepRules
{
    public sealed class LandscapeStepRule : IStepRule
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return Math.Abs(neighbour.Cost.CurrentCost - current.Cost.CurrentCost);
        }
    }
}