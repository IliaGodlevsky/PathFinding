using Pathfinding.Domain.Interface;

namespace Pathfinding.Service.Interface
{
    public interface IStepRule
    {
        double CalculateStepCost(IVertex neighbour, IVertex current);
    }
}