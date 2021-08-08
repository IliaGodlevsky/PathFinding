using Common.Interface;

namespace GraphLib.Interfaces
{
    public interface IVertexCost : ICloneable<IVertexCost>
    {
        int CurrentCost { get; }
    }
}
