using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System.Runtime.CompilerServices;

namespace Pathfinding.AlgorithmLib.Core.Realizations.StepRules
{
    public sealed class DefaultStepRule : IStepRule
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return neighbour.Cost.CurrentCost;
        }

        public override string ToString()
        {
            return "Default step rule";
        }
    }
}