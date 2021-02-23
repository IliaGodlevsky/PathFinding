using Common.Attributes;
using GraphLib.Interface;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    /// <summary>
    /// Respresents an empty graph, without any vertices
    /// </summary>
    [Default]
    public sealed class NullGraph : IGraph
    {
        public NullGraph()
        {

        }

        public IVertex this[ICoordinate position]
        {
            get => new DefaultVertex();
            set => _ = value;
        }

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public IEnumerable<IVertex> Vertices => new DefaultVertex[] { };

        public IVertex this[IEnumerable<int> coordinates]
        {
            get => new DefaultVertex();
            set => _ = value;
        }

        public IVertex this[int index]
        {
            get => new DefaultVertex();
            set => _ = value;
        }
    }
}
