using System;
using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// Represents a vertex of graph
    /// </summary>
    public interface IVertex : IEquatable<IVertex>
    {
        bool IsObstacle { get; set; }

        IVertexCost Cost { get; set; }

        ICollection<IVertex> Neighbours { get; set; }

        ICoordinate Position { get; }

        INeighboursCoordinates NeighboursCoordinates { get; }
    }
}