using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Core.Interface
{
    public interface IHeuristic
    {
        double Calculate(IVertex first, IVertex second);
    }
}