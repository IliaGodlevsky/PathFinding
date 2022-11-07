using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Core.Interface
{
    public interface IStepRule
    {
        double CalculateStepCost(IVertex neighbour, IVertex current);
    }
}