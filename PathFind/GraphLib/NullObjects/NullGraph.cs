using GraphLib.Interface;
using System.Collections.Generic;

namespace GraphLib.NullObjects
{
    /// <summary>
    /// Respresents an empty graph, without any vertices
    /// </summary>
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

        public bool IsDefault => true;

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

        public string GetFormattedData(string format)
        {
            return string.Empty;
        }
    }
}
