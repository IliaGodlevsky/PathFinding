using System;
using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IVertex : IEquatable<IVertex>
    {
        IGraph Graph { get; }

        bool IsObstacle { get; set; }

        IVertexCost Cost { get; set; }

        ICoordinate Position { get; }

        IReadOnlyCollection<IVertex> Neighbours { get; }
    }
}