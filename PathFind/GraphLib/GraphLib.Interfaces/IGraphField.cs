using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraphField
    {
        void Add(IVertex vertex);

        void Clear();

        IReadOnlyCollection<IVertex> Vertices { get; }
    }
}
