using Shared.Primitives.ValueRange;

namespace Pathfinding.GraphLib.Core.Interface
{
    public interface IVertexCost
    {
        InclusiveValueRange<int> CostRange { get; set; }

        int CurrentCost { get; set; }
    }
}
