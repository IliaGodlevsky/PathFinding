using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Smoothing.Interface
{
    public interface IMeanCost
    {
        int Calculate(IVertex neighbour, IVertex vertex);
    }
}
