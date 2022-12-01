using Shared.Primitives.ValueRange;

namespace Pathfinding.GraphLib.Core.Interface
{
    public interface IVertexCost
    {
        InclusiveValueRange<int> CostRange { get; }

        int CurrentCost { get; }

        IVertexCost SetCost(int cost);
    }
}
