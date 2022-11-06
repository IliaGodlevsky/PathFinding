using System;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Interface
{
    public interface ICoordinate : IReadOnlyList<int>, IEquatable<ICoordinate>
    {
    }
}
