using System;

namespace GraphLib.Interfaces
{
    public interface ICoordinate : IEquatable<ICoordinate>
    {
        int[] CoordinatesValues { get; }
    }
}
