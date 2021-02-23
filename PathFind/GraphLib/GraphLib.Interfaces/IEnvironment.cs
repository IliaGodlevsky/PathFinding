using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IEnvironment
    {
        IEnumerable<int[]> Environment { get; }
    }
}
