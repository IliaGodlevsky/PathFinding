﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System.Linq;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public sealed class VertexSerializationInfo
    {
        public bool IsObstacle { get; }

        public IVertexCost Cost { get; }

        public ICoordinate Position { get; }

        public ICoordinate[] Neighbourhood { get; }

        public VertexSerializationInfo(IVertex vertex)
            : this(vertex.IsObstacle,
                  vertex.Cost,
                  vertex.Position,
                  vertex.Neighbours.GetCoordinates().ToArray())
        {

        }

        public VertexSerializationInfo(bool isObstacle,
            IVertexCost cost,
            ICoordinate position,
            ICoordinate[] neighborhood)
        {
            IsObstacle = isObstacle;
            Cost = cost;
            Position = position;
            Neighbourhood = neighborhood;
        }
    }
}