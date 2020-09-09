using GraphLibrary.DTO;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace GraphLibrary.Graphs
{
    public class NullGraph : IGraph
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

        private NullGraph() => nullVertex = NullVertex.Instance;        
        public IVertex this[int width, int height] { get => nullVertex; set => nullVertex = NullVertex.Instance; }
        public IVertex End { get => nullVertex; set => nullVertex = NullVertex.Instance; }
        public int Height => 0;
        public int NumberOfVisitedVertices => 0;
        public int ObstacleNumber => 0;
        public int ObstaclePercent => 0;
        public int Size => 0;
        public IVertex Start { get => nullVertex; set => nullVertex = NullVertex.Instance; }
        public VertexDto[,] VerticesInfo => new VertexDto[0, 0];
        public int Width => 0;
        public IVertex[,] Array => new IVertex[0, 0];
        public IEnumerator<IVertex> GetEnumerator() => (IEnumerator<IVertex>)new IVertex[0, 0].GetEnumerator();
        public Point GetIndices(IVertex vertex) => new Point(0, 0);
        IEnumerator IEnumerable.GetEnumerator() => new IVertex[0, 0].GetEnumerator();

        private NullVertex nullVertex;
        private static NullGraph instance = null;
    }
}
