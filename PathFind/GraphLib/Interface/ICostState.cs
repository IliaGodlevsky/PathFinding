using GraphLib.VertexCost;
using System;

namespace GraphLib.Interface
{
    internal interface ICostState : ICloneable
    {
        string ToString(Cost cost);
    }
}
