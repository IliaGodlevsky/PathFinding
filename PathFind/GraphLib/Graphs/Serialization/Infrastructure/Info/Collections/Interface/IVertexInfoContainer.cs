using Common.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Info.Interface
{
    public interface IVertexInfoCollection : IEnumerable<VertexInfo>, IDefault
    {
        IEnumerable<int> DimensionsSizes { get; }
    }
}
