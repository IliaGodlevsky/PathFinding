using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraphField
    {
        IReadOnlyCollection<IVertex> Vertices { get; }
    }
}
