using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Interfaces
{
    public interface IGraphPath : IReadOnlyCollection<IVertex>
    {
        double Cost { get; }
    }
}
