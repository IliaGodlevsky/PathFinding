using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraphField<out TVertex> where TVertex : IVertex
    {
        IReadOnlyCollection<TVertex> Vertices { get; }
    }
}
