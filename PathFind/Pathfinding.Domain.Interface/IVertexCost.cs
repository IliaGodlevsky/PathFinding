using Shared.Primitives.ValueRange;

namespace Pathfinding.Domain.Interface
{
    public interface IVertexCost
    {
        InclusiveValueRange<int> CostRange { get; set; }

        int CurrentCost { get; set; }
    }
}
