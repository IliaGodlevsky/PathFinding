using GraphLibrary.Coordinates.Interface;
using GraphLibrary.Vertex.Cost;
using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.Info
{
    [Serializable]
    public sealed class VertexInfo
    {
        public VertexInfo(IVertex vertex)
        {
            IsObstacle = vertex.IsObstacle;
            Cost = (VertexCost)vertex.Cost.Clone();
            Position = (ICoordinate)vertex.Position.Clone();
        }

        public bool IsObstacle { get; set; }
        public VertexCost Cost { get; set; }
        public ICoordinate Position { get; set; }
    }
}
