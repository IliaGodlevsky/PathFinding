using System;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Interface
{
    public interface IVertex : IEquatable<IVertex>
    {
        bool IsObstacle { get; set; }

        ICoordinate Position { get; }

        IDictionary<IVertex, IVertexCost> Neighbours { get; set; }
    }
}