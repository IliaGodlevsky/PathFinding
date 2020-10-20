using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLibrary.Info.Interface
{
    public interface IVertexInfoCollection : IEnumerable<Info<IVertex>>
    {
        IEnumerable<int> DimensionsSizes { get; }
    }
}
