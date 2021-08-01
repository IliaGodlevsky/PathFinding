using Common.Interface;
using GraphLib.Realizations.VertexCost;
using System;

namespace GraphLib.Realizations.Interfaces
{
    internal interface ICostState : ICloneable<ICostState>
    {
        string ToString(WeightableVertexCost cost);
    }
}
