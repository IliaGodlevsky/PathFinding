using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.DTO
{
    [Serializable]
    public sealed class VertexDto
    {
        public VertexDto(IVertex vertex)
        {
            this.InitilizeBy(vertex);
        }

        public bool IsObstacle { get; private set; }
        public int Cost { get; private set; }
    }
}
