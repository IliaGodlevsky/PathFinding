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
    public sealed class NullGraph : IGraph
    {
        public static NullGraph Instance
        {
            get
            {
                if (instance == null)
                    instance = new NullGraph();
                return instance;
            }
        }

        private NullGraph()
        {
            nullVertex = NullVertex.Instance;
            array = new NullVertex[] { };
        }

        public IVertex this[ICoordinate position]
        {
            get => nullVertex;
            set => _ = value;
        }

        public IVertex End { get => nullVertex; set => _ = value; }
        public int NumberOfVisitedVertices => 0;
        public int ObstacleNumber => 0;
        public int ObstaclePercent => 0;
        public int Size => 0;
        public IVertex Start { get => nullVertex; set => _ = value; }
        public IVertexInfoCollection VertexInfoCollection => NullVertexInfoCollection.Instance;

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public IEnumerator<IVertex> GetEnumerator() => array.Cast<IVertex>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => array.GetEnumerator();
        public string GetFormattedData(string format) => string.Empty;

        private readonly IEnumerable<IVertex> array;
        private readonly NullVertex nullVertex;
        private static NullGraph instance = null;
    }
}
