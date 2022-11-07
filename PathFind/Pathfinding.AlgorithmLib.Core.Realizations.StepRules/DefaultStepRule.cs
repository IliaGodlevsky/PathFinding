using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Core.Realizations.StepRules
{
    public sealed class DefaultStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return neighbour.Cost.CurrentCost;
        }
    }
}