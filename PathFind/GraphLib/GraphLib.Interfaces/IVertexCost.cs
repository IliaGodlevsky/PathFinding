using ValueRange;

namespace GraphLib.Interfaces
{
    public interface IVertexCost
    {
        InclusiveValueRange<int> Range { get; }

        int CurrentCost { get; }
    }
}
