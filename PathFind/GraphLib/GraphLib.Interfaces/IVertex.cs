using System;
using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// An interface, that represents 
    /// a vertex of <see cref="IGraph"/>
    /// </summary>
    public interface IVertex : IEquatable<IVertex>
    {
        IGraph Graph { get; }

        /// <summary>
        /// Indicates whether the vertex is an obstacle or not
        /// </summary>
        bool IsObstacle { get; set; }

        IVertexCost Cost { get; set; }

        /// <summary>
        /// Represents verex's position in the <see cref="IGraph"/>
        /// </summary>
        ICoordinate Position { get; }

        /// <summary>
        /// Neighbours around the vertex
        /// </summary>
        IReadOnlyCollection<IVertex> Neighbours { get; }

        INeighboursCoordinates NeighboursCoordinates { get; }
    }
}