using System;

namespace GraphLib.Interface
{
    public interface IVertexCost : ICloneable
    {
        int CurrentCost { get; }
    }
}
