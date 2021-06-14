using GraphLib.Realizations.VertexCost;
using System;

namespace GraphLib.Realizations.Interfaces
{
    internal interface ICostState : ICloneable
    {
        string ToString(WeightableVertexCost cost);
    }
}
