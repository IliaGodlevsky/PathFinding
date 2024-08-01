using Pathfinding.Domain.Interface;

namespace Pathfinding.Service.Interface
{
    public interface IHeuristic
    {
        double Calculate(IVertex first, IVertex second);
    }
}