using Common.Interfaces;
using GraphLib.Info;
using System.Collections.Generic;

namespace GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface
{
    public interface IVertexInfoCollection : IEnumerable<VertexInfo>, IDefault
    {
        IEnumerable<int> DimensionsSizes { get; }
    }
}
