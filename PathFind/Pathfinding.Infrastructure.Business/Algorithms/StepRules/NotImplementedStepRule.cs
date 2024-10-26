using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using System;

namespace Pathfinding.Infrastructure.Business.Algorithms.StepRules
{
    public sealed class NotImplementedStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            throw new NotImplementedException();
        }
    }
}
