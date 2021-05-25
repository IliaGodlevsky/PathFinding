using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface INeighboursCoordinates
    {
        IEnumerable<ICoordinate> Coordinates { get; }
    }
}
