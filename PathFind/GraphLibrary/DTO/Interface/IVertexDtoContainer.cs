using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLibrary.DTO.Interface
{
    public interface  IVertexDtoContainer : IEnumerable<Dto<IVertex>>
    {
        IEnumerable<int> DimensionsSizes { get; }
    }
}
