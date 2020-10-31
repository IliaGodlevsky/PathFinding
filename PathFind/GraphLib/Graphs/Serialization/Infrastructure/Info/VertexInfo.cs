using GraphLib.Coordinates.Interface;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using System;

namespace GraphLib.Info
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
