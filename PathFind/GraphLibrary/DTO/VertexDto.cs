using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.DTO
{
    [Serializable]
    public sealed class VertexDto
    {
        public VertexDto(IVertex vertex)
        {
            IsObstacle = vertex.IsObstacle;
            Cost = vertex.Cost;
        }

        public bool IsObstacle { get; private set; }
        public int Cost { get; private set; }
    }
}
