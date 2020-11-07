using GraphLib.Coordinates.Interface;
using GraphLib.Graphs.Abstractions;
using GraphLib.Info.Containers;
using GraphLib.Info.Interface;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs
{
    /// <summary>
    /// An empty graph. Returns instead of 
    /// real instance of Graph class instead of null
    /// </summary>
    public sealed class DefaultGraph : IGraph
    {
        public DefaultGraph()
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
        public int NumberOfVisitedVertices => 0;
        public int ObstacleNumber => 0;
        public int ObstaclePercent => 0;
        public int Size => 0;
        public IVertex Start { get => defaultVertex; set => _ = value; }
        public IVertexInfoCollection VertexInfoCollection => new EmptyVertexInfoCollection();

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public bool IsDefault => true;

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
