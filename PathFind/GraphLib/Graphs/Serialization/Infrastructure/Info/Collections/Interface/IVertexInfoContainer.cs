using GraphLib.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLib.Info.Interface
{
    public interface IVertexInfoCollection : IEnumerable<VertexInfo>
    {
        IEnumerable<int> DimensionsSizes { get; }
    }
}
