using Common.Interface;
using System;

namespace GraphLib.Interfaces
{
    public interface IVertexCost : ICloneable<IVertexCost>
    {
        int CurrentCost { get; }
    }
}
