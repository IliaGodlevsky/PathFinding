using Common.Interface;
using GraphLib.Realizations.VertexCost;

namespace GraphLib.Realizations.Interfaces
{
    internal interface ICostState : ICloneable<ICostState>
    {
        string ToString(WeightableVertexCost cost);
    }
}
