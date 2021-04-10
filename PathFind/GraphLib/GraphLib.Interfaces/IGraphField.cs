using System.Collections.Generic;

namespace GraphLib.Interface
{
    public interface IGraphField
    {
        void Add(IVertex vertex);
        void Clear();

        IEnumerable<IVertex> Vertices { get; }
    }
}
