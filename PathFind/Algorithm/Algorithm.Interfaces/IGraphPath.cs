using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Interfaces
{
    public interface IGraphPath : IReadOnlyCollection<ICoordinate>
    {
        double Cost { get; }
    }
}
