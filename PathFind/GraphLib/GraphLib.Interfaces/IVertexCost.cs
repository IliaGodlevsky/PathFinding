using System;

namespace GraphLib.Interfaces
{
    public interface IVertexCost : ICloneable
    {
        int CurrentCost { get; }
    }
}
