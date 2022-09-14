using System;
using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface ICoordinate : IReadOnlyList<int>, IEquatable<ICoordinate>
    {
    }
}
