using System.Collections.Generic;

namespace GraphLibrary.DTO.Interface
{
    public interface  IVertexDtoContainer : IEnumerable<VertexDto>
    {
        IEnumerable<int> DimensionsSizes { get; }
    }
}
