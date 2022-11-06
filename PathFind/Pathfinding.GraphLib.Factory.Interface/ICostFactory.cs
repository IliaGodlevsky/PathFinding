using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface IVertexCostFactory
    {
        IVertexCost CreateCost(int cost);

        IVertexCost CreateCost();
    }
}
