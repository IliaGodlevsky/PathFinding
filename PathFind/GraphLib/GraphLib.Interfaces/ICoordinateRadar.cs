using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface ICoordinateRadar
    {
        IEnumerable<ICoordinate> Environment { get; }
    }
}
