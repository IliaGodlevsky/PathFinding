using GraphLibrary.Vertex;
using System;

namespace GraphLibrary
{
    [Serializable]
    public class VertexInfo
    {
        public VertexInfo(IVertex vertex)
        {
            IsObstacle = vertex.IsObstacle;            
            Cost = vertex.Cost;
        }

        public bool IsObstacle { get; private set; }
        public int Cost { get; private set; }
    }
}
