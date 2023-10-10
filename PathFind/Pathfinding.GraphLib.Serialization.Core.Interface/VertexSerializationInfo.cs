using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public sealed class VertexSerializationInfo
    {
        public bool IsObstacle { get; }

        public ICoordinate Position { get; }

        public IReadOnlyDictionary<ICoordinate, IVertexCost> Neighbourhood { get; }

        public VertexSerializationInfo(IVertex vertex)
            : this(vertex.IsObstacle,
                  vertex.Position,
                  vertex.Neighbours
                  .ToDictionary(item=>item.Key.Position, item=> item.Value)
                  .AsReadOnly())
        {

        }

        public VertexSerializationInfo(bool isObstacle,
            ICoordinate position,
            IReadOnlyDictionary<ICoordinate, IVertexCost> neighborhood)
        {
            IsObstacle = isObstacle;
            Position = position;
            Neighbourhood = neighborhood;
        }
    }
}