using Common.Interface;

namespace GraphLib.Realizations.Interfaces
{
    internal interface ICostState : ICloneable<ICostState>
    {
        string ToString(VertexCost cost);
    }
}
