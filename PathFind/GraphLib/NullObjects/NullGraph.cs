using GraphLib.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.NullObjects
{
    /// <summary>
    /// Respresents an empty graph, without any vertices
    /// </summary>
    public sealed class NullGraph : IGraph
    {
        public NullGraph()
        {
            defaultVertex = new DefaultVertex();
            array = new DefaultVertex[] { };
        }

        public IVertex this[ICoordinate position]
        {
            get => defaultVertex;
            set => _ = value;
        }

        public IVertex End { get => defaultVertex; set => _ = value; }

        public IVertex Start { get => defaultVertex; set => _ = value; }

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public bool IsDefault => true;

        public IVertex this[int index]
        {
            get => defaultVertex;
            set => _ = value;
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            return array.Cast<IVertex>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return array.GetEnumerator();
        }

        public string GetFormattedData(string format)
        {
            return string.Empty;
        }

        private readonly IEnumerable<IVertex> array;
        private readonly DefaultVertex defaultVertex;
    }
}
