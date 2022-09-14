using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Interfaces
{
    public interface IAlgorithm<out TPath>
        where TPath : IEnumerable<IVertex>
    {
        TPath FindPath();
    }
}
