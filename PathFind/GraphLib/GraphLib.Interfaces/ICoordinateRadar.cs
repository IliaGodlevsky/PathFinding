using System.Collections.Generic;

namespace GraphLib.Interface
{
    public interface ICoordinateRadar
    {
        IEnumerable<int[]> Environment { get; }
    }
}
