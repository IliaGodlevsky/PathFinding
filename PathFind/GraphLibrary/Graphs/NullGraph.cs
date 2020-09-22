using GraphLibrary.DTO;
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
            Array = new NullVertex[0, 0];
        }

        public IVertex this[int width, int height] 
        { 
            get => nullVertex; 
            set => nullVertex = NullVertex.Instance; 
        }

        public IVertex this[Position position]
        { 
            get => nullVertex; 
            set => nullVertex = NullVertex.Instance; 
        }

        public IVertex End { get => nullVertex; set => nullVertex = NullVertex.Instance; }
        public int Height => 0;
        public int NumberOfVisitedVertices => 0;
        public int ObstacleNumber => 0;
        public int ObstaclePercent => 0;
        public int Size => 0;
        public IVertex Start { get => nullVertex; set => nullVertex = NullVertex.Instance; }
        public VertexDto[,] VerticesDto => new VertexDto[0, 0];
        public int Width => 0;
        public IVertex[,] Array { get; }

        

        public IEnumerator<IVertex> GetEnumerator() => Array.Cast<IVertex>().ToList().GetEnumerator();
        public Position GetIndices(IVertex vertex) => new Position(0, 0);
        IEnumerator IEnumerable.GetEnumerator() => Array.GetEnumerator();

        private NullVertex nullVertex;
        private static NullGraph instance = null;
    }
}
