using System.Collections.Generic;

namespace GraphLib.Interface
{
    public interface IGraphPath
    {
        IEnumerable<IVertex> Path { get; }
    }
}
