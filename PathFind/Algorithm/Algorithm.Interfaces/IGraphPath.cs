using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Interfaces
{
    public interface IGraphPath
    {
        IEnumerable<IVertex> Path { get; }

        double PathCost { get; }
    }
}
