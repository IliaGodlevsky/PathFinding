using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Interfaces
{
    public interface IGraphPath
    {
        IReadOnlyList<IVertex> Path { get; }

        int Length { get; }

        double Cost { get; }
    }
}
