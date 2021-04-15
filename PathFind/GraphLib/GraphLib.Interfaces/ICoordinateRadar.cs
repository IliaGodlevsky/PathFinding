using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface ICoordinateRadar
    {
        IEnumerable<int[]> Environment { get; }
    }
}
