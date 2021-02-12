using GraphLib.Interface;
using System;

namespace GraphLib.Infrastructure
{
    [Serializable]
    public sealed class VertexSerializationInfo
    {
        public VertexSerializationInfo(IVertex vertex)
        {
            if (vertex.Cost is ICloneable cost 
                && vertex.Position is ICloneable position)
            {
                Cost = (IVertexCost)cost.Clone();
                Position = (ICoordinate)position.Clone();
                IsObstacle = vertex.IsObstacle;
            }
            else
            {
                throw new Exception();
            }
        }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public ICoordinate Position { get; set; }
    }
}
