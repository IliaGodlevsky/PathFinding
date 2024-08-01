using System;
using System.Collections.Generic;

namespace Pathfinding.Domain.Interface
{
    public interface ICoordinate : IReadOnlyList<int>, IEquatable<ICoordinate>
    {
    }
}
