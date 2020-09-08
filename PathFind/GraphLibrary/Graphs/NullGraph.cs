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
        public static NullGraph GetInstance()
        {
            if (instance == null)
                instance = new NullGraph();
            return instance;
        }
        private NullGraph() => mockVertex = NullVertex.GetInstance();        
        public IVertex this[int width, int height] { get => mockVertex; set => mockVertex = NullVertex.GetInstance(); }
        public IVertex End { get => mockVertex; set => mockVertex = NullVertex.GetInstance(); }
        public int Height => 0;
        public int NumberOfVisitedVertices => 0;
        public int ObstacleNumber => 0;
        public int ObstaclePercent => 0;
        public int Size => 0;
        public IVertex Start { get => mockVertex; set => mockVertex = NullVertex.GetInstance(); }
        public VertexInfo[,] VerticesInfo => new VertexInfo[0, 0];
        public int Width => 0;
        public IVertex[,] Array => new IVertex[0, 0];
        public IEnumerator<IVertex> GetEnumerator() => (IEnumerator<IVertex>)new IVertex[0, 0].GetEnumerator();
        public Point GetIndices(IVertex vertex) => new Point(0, 0);
        IEnumerator IEnumerable.GetEnumerator() => new IVertex[0, 0].GetEnumerator();

        private NullVertex mockVertex;
        private static NullGraph instance = null;
    }
}
