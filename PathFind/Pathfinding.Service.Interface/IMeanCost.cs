using Pathfinding.Domain.Interface;

namespace Pathfinding.Service.Interface
{
    public interface IMeanCost
    {
        int Calculate(IVertex neighbour, IVertex vertex);
    }
}
