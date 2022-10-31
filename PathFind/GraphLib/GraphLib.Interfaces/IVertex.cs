using System;
using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IVertex : IEquatable<IVertex>
    {
        bool IsObstacle { get; set; }

        IVertexCost Cost { get; set; }

        ICoordinate Position { get; }

        IReadOnlyCollection<IVertex> Neighbours { get; set; }
    }
}