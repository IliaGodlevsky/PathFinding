using GraphLibrary.Coordinates.Interface;
using GraphLibrary.DTO;
using GraphLibrary.DTO.Containers;
using GraphLibrary.DTO.Interface;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Graphs
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
        public IVertexDtoContainer VertexDtos => NullVertexDtoContainer.Instance;

        public IEnumerable<int> DimensionsSizes => new int[] { };

        public IEnumerator<IVertex> GetEnumerator() => array.Cast<IVertex>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => array.GetEnumerator();
        public string GetFormattedData(string format) => string.Empty;

        private readonly IEnumerable<IVertex> array;
        private readonly NullVertex nullVertex;
        private static NullGraph instance = null;
    }
}
