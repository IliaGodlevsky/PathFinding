using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Algorithms.StepRules
{
    public sealed class DefaultStepRule : IStepRule
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return neighbour.Cost.CurrentCost;
        }
    }
}